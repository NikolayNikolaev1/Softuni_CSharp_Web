namespace SocialNetwork.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class TagAttribute : ValidationAttribute
    {
        public TagAttribute()
        {

        }

        public override bool IsValid(object value)
        {
            string tag = value as string;

            Regex regex = new Regex("^#(\\w)+$");

            return regex.Match(tag).Success;
        }
    }
}
