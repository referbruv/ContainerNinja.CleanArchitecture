namespace ContainerNinja.Contracts.DTO
{
    public class BaseResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
    }
}