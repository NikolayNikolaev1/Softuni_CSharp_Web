namespace LearningSystem.Common
{
    public static class GlobalConstants
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
            public const string BlogAuthor = "Blog Author";
            public const string Trainer = "Trainer";
        }

        public static class Properties
        {
            public const int ArticleContentMaxLength = 300;
            public const int ArticleTitleMaxLength = 50;
            public const int CourseDescriptionMaxLength = 300;
            public const int CourseNameMaxLength = 50;
            public const int UserNameMaxLength = 50;
            public const int UsernameMinLength = 3;
        }
    }
}
