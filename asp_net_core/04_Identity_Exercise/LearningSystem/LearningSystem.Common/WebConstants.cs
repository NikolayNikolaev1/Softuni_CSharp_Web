namespace LearningSystem.Common
{
    public static class WebConstants
    {
        public static class AdminCredentials
        {
            public const string Email = "admin@learningsystem.test";
            public const string Password = "admin123";
            public const string Username = "admin";
        }

        public static class ErrorMessages
        {
            public const string InvalidUserName = "Username must have between 3 and 50 symbols.";
        }

        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string BlogAuthor = "BlogAuthor";
            public const string Trainer = "Trainer";
        }

        public const string TempDataSuccessMessageKey = "SuccessMessage";
    }
}
