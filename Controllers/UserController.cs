﻿using EcommerceBackend.Contracts.Formats;
using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Contracts.Response;
using EcommerceBackend.Helpers;
using EcommerceBackend.Resource.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserResource _userResource;
        private readonly ILoginResource _loginResource;
        private readonly IConfiguration _configuration;
        public UserController(IUserResource userResource , ILoginResource loginResource , IConfiguration configuration)
        {
            _userResource = userResource;
           _loginResource = loginResource;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {              
                var employees = await _userResource.GetAllEmployees();
                return Ok(new MessageResponseList<UserResponse>(true, "Lista de empleados", employees));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/autorize")]
        public async Task<IActionResult> GetAutoRize()
        {
            try
            {
                var employees = await _userResource.GetAllEmployees();
                return Ok(new MessageResponseList<UserResponse>(true, "Lista de empleados", employees));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var employe = await _userResource.GetById(id);
                if (employe != null)
                {
                    return Ok(new MessageResponse(true, "Empledo encontrado :)", employe));
                }
                return NotFound(new MessageResponse(false, "Empledo no encontado...", null));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(Guid userId)
        {
            try
            {
                var employe = _userResource.DeleteEmployee(userId);
                if (!employe)
                {
                    return NotFound(new MessageResponse(false, "Error al eliminar Empleado", null));
                }
                return Ok(new MessageResponse(true, "Empledo eliminado...", null));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(UserParams userParams)
        {
            userParams.Credentials.Validate();

            var userResponse = await _userResource.CreateEmployee(userParams);
            if(userResponse != null)
            {
                return Ok(new MessageResponse(true,"Usuario creado",userResponse));
            }
            return BadRequest(new MessageResponse(false,"Error al crear usuario",null));
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(UserParams userParams, Guid userId)
        {
            try
            {
                var employee = await _userResource.UpdateEmployee(userParams, userId);
                if (employee != null) return Ok(new MessageResponse(true, "User updated...", employee));
                return BadRequest(new MessageResponse(true, "User updated...", null));
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageResponse(false, ex.Message, null));
            }

        }
    }
}
