namespace ContainerNinja.Contracts.DTO
{
    public class AuthTokenDTO
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}