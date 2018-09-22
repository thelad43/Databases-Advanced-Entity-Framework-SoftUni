﻿namespace PhotoShare.App.Utilities
{
    using System;

    public static class TagUtilities
    {
        private const int DefaultLength = 20;

        /// <summary>
        /// Transforms given wrong tag to a valid one by:
        /// 1. removing all white spaces
        /// 2. adding pound ('#') sign if needed
        /// 3. reduces its length to 20 symbols if it is more
        /// </summary>
        /// <param name="wrongTag">wrong tag to be converted</param>
        /// <returns>converted tag validated by given rules</returns>
        public static string ValidateOrTransform(this string wrongTag)
        {
            if (string.IsNullOrWhiteSpace(wrongTag))
            {
                throw new InvalidOperationException("Cannot convert empty string to a valid tag");
            }

            var transformedTag = wrongTag;

            transformedTag = RemoveAllWhiteSpaces(transformedTag);

            if (wrongTag[0] != '#')
            {
                transformedTag = AppendPoundSign(transformedTag);
            }

            if (transformedTag.Length > DefaultLength)
            {
                transformedTag = ReduceStringLength(transformedTag, DefaultLength);
            }

            return transformedTag;
        }

        private static string RemoveAllWhiteSpaces(string tag)
        {
            var newTag = tag.Replace(" ", string.Empty)
                .Replace("\t", string.Empty)
                .Replace("\n", string.Empty)
                .Replace("\r", string.Empty);

            return newTag;
        }

        private static string AppendPoundSign(string tag)
        {
            return "#" + tag;
        }

        private static string ReduceStringLength(string tag, int length)
        {
            var reducedString = tag.Substring(0, length);
            return reducedString;
        }
    }
}