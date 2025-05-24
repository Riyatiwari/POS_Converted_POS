using Microsoft.AspNetCore.Http;
using System;
using System.Text;

namespace Converted_POS.Helpers
{
    /// <summary>
    /// Helper class for working with Session to avoid ambiguous method calls
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        /// Gets a string from the session
        /// </summary>
        public static string GetSessionString(this ISession session, string key)
        {
            return session.Get(key) == null ? null : System.Text.Encoding.UTF8.GetString(session.Get(key));
        }

        /// <summary>
        /// Sets a string in the session
        /// </summary>
        public static void SetSessionString(this ISession session, string key, string value)
        {
            session.Set(key, System.Text.Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Gets an integer from the session
        /// </summary>
        public static int? GetSessionInt32(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null || data.Length < 4)
            {
                return null;
            }
            return BitConverter.ToInt32(data, 0);
        }

        /// <summary>
        /// Sets an integer in the session
        /// </summary>
        public static void SetSessionInt32(this ISession session, string key, int value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }
    }
} 