namespace AccessControlApi.Application.Eceptions
{
    public class ForbiddenException : CustomException
    {
        public ForbiddenException()
        {

        }
        public ForbiddenException(string message) : base(message)
        {

        }
    }
}
