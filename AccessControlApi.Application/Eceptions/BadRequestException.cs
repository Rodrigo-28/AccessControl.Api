namespace AccessControlApi.Application.Eceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException()
        {

        }
        public BadRequestException(string message) : base(message)
        {

        }
    }
}
