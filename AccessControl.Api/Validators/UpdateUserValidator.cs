using AccessControlApi.Application.Dtos.Requests;
using FluentValidation;

namespace AccessControlApi.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Username)
                 .MinimumLength(3)
                 .When(x => !string.IsNullOrWhiteSpace(x.Username));

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .When(x => !string.IsNullOrWhiteSpace(x.Password));

            RuleFor(x => x.RoleId)
                .GreaterThan(0)
                .When(x => x.RoleId.HasValue);

            RuleFor(x => x)
                .Must(HasAtLeastOneField)
                .WithMessage("At least one field must be sent for updating");
        }
        private bool HasAtLeastOneField(UpdateUserDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Username)
                 || !string.IsNullOrWhiteSpace(dto.Password)
                 || dto.RoleId.HasValue;
        }
    }
}
