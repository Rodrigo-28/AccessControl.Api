using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;
using AccessControlApi.Application.Eceptions;
using AccessControlApi.Application.Interfaces;
using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AccessControlApi.Application.Services
{
    public class UserService
     : BaseService<User, UserResponseDto, CreateUserDto, UpdateUserDto>,
       IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncryptionService _passwordEncryptionService;
        private readonly IRolRepository _rolRepository;

        public UserService(IUserRepository userRepository,
        IMapper mapper,
        IPasswordEncryptionService passwordEncryptionService,
        IRolRepository rolRepository) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _passwordEncryptionService = passwordEncryptionService;
            _rolRepository = rolRepository;
        }


        public override async Task<UserResponseDto> Create(CreateUserDto createUserDto)
        {
            var userExist = await _userRepository.GetOneByEmail(createUserDto.Email);
            if (userExist != null)
            {
                throw new BadRequestException($"Already exist an user with this email {createUserDto.Email}")
                {
                    ErrorCode = "006"
                };
            }
            var user = _mapper.Map<User>(createUserDto);
            user.Password = _passwordEncryptionService.HashPassword(createUserDto.Password);
            var created = await _userRepository.Create(user);
            var userWithRole = await _userRepository.GetOneWithRole(created.Id);
            return _mapper.Map<UserResponseDto>(userWithRole);
        }




        public override async Task<IEnumerable<UserResponseDto>> GetAll()
        {
            var users = await _userRepository.GetAll(q =>
                  q.Include(u => u.Role)
              );
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto> GetOne(int userId)
        {
            var user = await _userRepository.GetOne(userId, q => q.Include(u => u.Role));
            if (user == null)
            {
                throw new NotFoundException($"user of id {userId} does not exist")
                {
                    ErrorCode = "005"
                };
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<User> GetOneByEmail(string email)
        {
            var user = await _userRepository.GetOneByEmail(email);
            if (user == null)
            {
                throw new NotFoundException()
                {
                    ErrorCode = "004"
                };
            }
            return user;
        }



        public async Task<UserResponseDto> Update(int userId, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetOne(userId);

            if (user == null)
            {
                throw new NotFoundException($"user of id {userId} does not exist")
                {
                    ErrorCode = "005"
                };
            }
            if (updateUserDto.Password != null)
            {
                updateUserDto.Password =
                    _passwordEncryptionService.HashPassword(updateUserDto.Password);
            }
            if (updateUserDto.RoleId.HasValue)
            {
                var roleExists = await _rolRepository.GetOne(updateUserDto.RoleId.Value);
                if (roleExists == null)
                {
                    throw new BadRequestException("Invalid role");
                }
                user.RoleId = updateUserDto.RoleId.Value;
            }
            _mapper.Map(updateUserDto, user);

            var updated = await _userRepository.Update(user);

            return _mapper.Map<UserResponseDto>(updated);
        }

        public async Task<bool> VerifyPassword(int userId, string password)
        {
            var user = await _userRepository.GetOne(userId);
            if (user == null)
            {
                throw new BadRequestException("user not exists")
                {
                    ErrorCode = "000"
                };
            }
            var isValid = _passwordEncryptionService.VerifyPassword(user.Password, password);
            return isValid;
        }
        public async Task<GenericResponseDto> ChangePassword(int userId, string password)
        {
            var user = await _userRepository.GetOne(userId);
            if (user == null)
            {
                throw new BadRequestException("user not exists")
                {
                    ErrorCode = "000"
                };
            }
            var hashedPassword = _passwordEncryptionService.HashPassword(password);
            user.Password = hashedPassword;
            if (user.FirstLogin)
            {
                user.FirstLogin = false;
            }
            await _userRepository.Update(user);


            return new GenericResponseDto { Success = true };
        }
    }
}
