namespace SocialNetwork.Validations
{
    using System;
    using System.Linq;
    using System.Text;

    public static class TagTransformer
    {
        public static string TransformTag(string tag)
        {
            StringBuilder sb = new StringBuilder("#");
            return sb
                .Append(tag.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray())
                .ToString()
                .Substring(0, 20);
        }
    }
}
