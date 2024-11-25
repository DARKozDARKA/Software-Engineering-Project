using System;
using UnityEngine;

namespace CodeBase.Tools
{
    public static class Extensions
    {
        public static T With<T>(this T self, Action<T> set)
        {
            set.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> apply, Func<bool> when)
        {
            if (when())
                apply?.Invoke(self);

            return self;
        }

        public static T With<T>(this T self, Action<T> apply, bool when)
        {
            if (when)
                apply?.Invoke(self);

            return self;
        }

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static Vector2 ToVector2(this Vector3 vector) => 
            new Vector2(vector.x, vector.y);
        
        public static Vector3 ToVector3(this Vector2 vector) => 
            new Vector3(vector.x, vector.y, 0);
    }
}