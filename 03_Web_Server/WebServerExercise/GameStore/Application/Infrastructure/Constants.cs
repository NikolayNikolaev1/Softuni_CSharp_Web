namespace GameStore.Application.Infrastructure
{
    public static class Constants
    {
        public class ErrorMessages
        {
            public const string EmptyFields = "You have empty fields.";

            public const string InvalidGameDescription = "Description length must be more than 20 symbols.";

            public const string InvalidGamePrice = "Price can not be negative.";

            public const string InvalidGameSize = "Size can not be negative.";

            public const string InvalidGameThumbnailUrl = "Invalid Thumbnail URL.";

            public const string InvalidGameTitle = "Invalid Title length. It must be between 3 and 100 symbols.";

            public const string InvalidGameTrailer = "Invalid YouTube video ID.";

            public const string InvalidUserConfirmPassword = "Invalid Passwords. Password and Confirm Password should be the same.";

            public const string InvalidUserEmail = "Invalid Email. It should contain '@' and '.' symbols.";

            public const string InvalidUserPassword = "Invalid Password. It should be at least 6 symbols long, containing 1 uppercase letter, 1 lowercase letter and 1 digit.";

            public const string UserExists = "Invalid User. User with that email already exists.";

            public const string UserNotExist = "Invalid User. User with that name and password does not exist.";
        }

        public class FilePaths
        {
            public const string GameAdd = @"admin\game\add";

            public const string GameDelete = @"admin\game\delete";

            public const string GameEdit = @"admin\game\edit";

            public const string GameList = @"admin\game\list";

            public const string HomeIndex = @"home\index";

            public const string UserLogin = @"user\login";

            public const string UserRegister = @"user\register";
        }

        public class SessionKeys
        {
            public const string CurrentUser = "^%Current_User_Email%^";
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
    }
}
