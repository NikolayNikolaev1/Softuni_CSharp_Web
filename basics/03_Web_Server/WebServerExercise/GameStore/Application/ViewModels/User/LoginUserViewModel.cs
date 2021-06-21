namespace GameStore.Application.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.Constants;

    public class LoginUserViewModel
    {
        [Required(ErrorMessage = ErrorMessages.EmptyFields)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmptyFields)]
        public string Password { get; set; }
    }
}
