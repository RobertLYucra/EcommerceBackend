﻿using EcommerceBackend.Domain;
using System.Net;

namespace EcommerceBackend.Contracts.Params
{
    public class UserParams
    {
        public string Rol { get; set; }
        public UserLogin?  Credentials { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DNI { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
    }
}
