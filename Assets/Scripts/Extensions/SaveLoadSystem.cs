using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace LuckyJet
{
    public class SaveLoadSystem
    {
        public static void Save<T>(T data) where T : ISaveData
        {
            string contents = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(GetPath<T>(), contents);
        }

        public static T Load<T>() where T : ISaveData
        {
            string path = GetPath<T>();
            if (!File.Exists(path))
                return default(T);
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private static string GetPath<T>() where T : ISaveData
        {
            return Application.persistentDataPath + "/" + typeof(T).Name + ".json";
        }

        public static void Check<T>() where T : ISaveData
        {
            if (Load<T>() == null)
                Save((T)Activator.CreateInstance(typeof(T), 0));
        }
    }
}
