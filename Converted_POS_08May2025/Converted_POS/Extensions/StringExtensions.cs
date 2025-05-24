using System;

namespace Converted_POS.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if a string has a value (is not null or empty)
        /// </summary>
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Gets the string value or null if the string is empty
        /// </summary>
        public static string Value(this string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }

        /// <summary>
        /// Safely converts a string to an integer, returning null if conversion fails
        /// </summary>
        public static int? ToNullableInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (int.TryParse(value, out int result))
                return result;

            return null;
        }

        /// <summary>
        /// Safely converts a string to a decimal, returning null if conversion fails
        /// </summary>
        public static decimal? ToNullableDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (decimal.TryParse(value, out decimal result))
                return result;

            return null;
        }

        /// <summary>
        /// Safely converts a string to a DateTime, returning null if conversion fails
        /// </summary>
        public static DateTime? ToNullableDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (DateTime.TryParse(value, out DateTime result))
                return result;

            return null;
        }
    }
} 