namespace AccessControlApi.Application.Eceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
