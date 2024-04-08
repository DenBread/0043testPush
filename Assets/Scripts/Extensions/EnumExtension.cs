using System;

namespace LuckyJet
{
    public static class EnumExtension
    {
        public static T SetRandom<T>() where T : Enum
        {
            var array = Enum.GetValues(typeof(T));
            return (T)array.GetValue(new System.Random().Next(0, array.Length));
        }
    }
}
