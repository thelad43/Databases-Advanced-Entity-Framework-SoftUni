namespace PhotoShare.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class TagAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var pattern = "#[a-zA-Z0-9]{2,20}";

            var regex = new Regex(pattern);

            if (!regex.IsMatch(value.ToString()))
            {
                return false;
            }

            return true;
        }
    }
}