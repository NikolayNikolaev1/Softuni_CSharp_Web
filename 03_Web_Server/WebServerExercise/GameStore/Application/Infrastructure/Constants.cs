namespace GameStore.Application.Infrastructure
{
    public static class Constants
    {
        public class ErrorMessages
        {
            public const string EmptyFields = "You have empty fields.";

            public const string InvalidConfirmPassword = "Invalid Passwords. Password and Confirm Password should be the same.";

            public const string InvalidEmail = "Invalid Email. It should contain '@' and '.' symbols.";

            public const string InvalidPassword = "Invalid Password. It should be at least 6 symbols long, containing 1 uppercase letter, 1 lowercase letter and 1 digit.";

            public const string UserExists = "Invalid User. User with that email already exists.";

            public const string UserNotExist = "Invalid User. User with that name and password does not exist.";
        }

        public class FilePaths
        {
            public const string GameAdd = @"admin\game\add";

            public const string GameList = @"admin\game\list";

            public const string HomeIndex = @"home\index";

            public const string UserLogin = @"user\login";

            public const string UserRegister = @"user\register";
        }

        public class UrlPaths
        {
            public const string GameAdd = "/game/add";

            public const string GameDelete = "/game/delete/{(?<id>[0-9]+)}";

            public const string GameEdit = "/game/edit/{(?<id>[0-9]+)}";

            public const string GameAdminList = "/admin/games";

            public const string Home = "/";

            public const string Login = "/login";

            public const string Logout = "/logout";

            public const string Register = "/register";

        }

        public class SessionKeys
        {
            public const string CurrentUser = "^%Current_User_Email%^";
        }

    }
}
