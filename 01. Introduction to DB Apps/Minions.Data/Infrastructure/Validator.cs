namespace Minions.Data.Infrastructure
{
    using System;

    public static class Validator
    {
        public static void ThrowExceptionIfNullOrWhitespace(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new InvalidOperationException($"Field {text} cannot be null or whitespace.");
            }
        }
    }
}