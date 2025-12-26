using AccessControlApi.Domian.Models;

namespace AccessControlApi.Domian.Interfaces
{
    public interface IJwtTokenService
    {
        public string GenerateJwtToken(User user);
    }
}
