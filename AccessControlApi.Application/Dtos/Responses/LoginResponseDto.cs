namespace AccessControlApi.Application.Dtos.Responses
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public bool FirstLogin { get; set; }
    }
}
