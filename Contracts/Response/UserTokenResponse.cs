namespace EcommerceBackend.Contracts.Response
{
    public class UserTokenResponse
    {
       public  UserResponse UserResponse { get; set; }
       public string Token { get; set; }
        public DateTime ExpirationToken { get; set; }
    }
}
