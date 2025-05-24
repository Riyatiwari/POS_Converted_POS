using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace Converted_POS.Extensions
{
    public static class CustomSessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public static int? GetSessionInt32(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null || data.Length < 4)
            {
                return null;
            }
            return BitConverter.ToInt32(data, 0);
        }

        public static void SetSessionInt32(this ISession session, string key, int value)
        {
            var bytes = BitConverter.GetBytes(value);
            session.Set(key, bytes);
        }
    }
} 