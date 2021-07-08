namespace LearningSystem.Data.Common
{
    public static class Constants
    {
        public static class Properties
        {
            public const int ArticleContentMaxLength = 300;
            public const int ArticleTitleMaxLength = 50;
            public const int CourseDescriptionMaxLength = 300;
            public const int CourseNameMaxLength = 50;
            public const int UserNameMaxLength = 50;
            public const int UsernameMinLength = 3;
        }

        public static class ErrorMessages
        {
            public const string InvalidUserName = "Username must have between 3 and 50 symbols.";
        }
    }
}
