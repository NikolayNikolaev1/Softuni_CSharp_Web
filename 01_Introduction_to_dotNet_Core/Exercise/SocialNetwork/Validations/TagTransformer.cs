namespace SocialNetwork.Validations
{
    using System;
    using System.Linq;
    using System.Text;

    public class TagTransformer
    {
        public string TrasnformTag(string tag)
        {
            StringBuilder sb = new StringBuilder("#");
            return sb
                .Append(tag.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray())
                .ToString();
        }
    }
}
