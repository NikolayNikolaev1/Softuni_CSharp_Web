namespace GameStore.Application.ViewModels.User
{
    using Infrastructure;
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.Constants;

    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = ErrorMessages.EmptyFields)]
        [MaxLength(Validations.User.EmailMaxLength, ErrorMessage = ErrorMessages.InvalidUserEmail)]
        [EmailAddress(ErrorMessage = ErrorMessages.InvalidUserEmail)]
        public string Email { get; set; }

        public string FullName { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmptyFields)]
        [MinLength(Validations.User.PasswordMinLength, ErrorMessage = ErrorMessages.InvalidUserPassword)]
        [MaxLength(Validations.User.PasswordMaxLength, ErrorMessage = ErrorMessages.InvalidUserPassword)]
        [Password(ErrorMessage = ErrorMessages.InvalidUserPassword)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = ErrorMessages.InvalidUserConfirmPassword)]
        public string ConfirmPassword { get; set; }
    }
}
