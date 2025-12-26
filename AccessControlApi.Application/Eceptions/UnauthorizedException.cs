namespace AccessControlApi.Application.Eceptions
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException()
        {

        }
        public UnauthorizedException(string message) : base(message)
        {

        }
    }
}
