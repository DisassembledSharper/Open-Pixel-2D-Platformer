using UnityEngine;
using System.IO;
using Data;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace Managers
{
    public static class SaveManager
    {
        private static string path = Application.persistentDataPath + "/data.sav";

        public static void SaveData(GameData data)
        {
            string content = JsonUtility.ToJson(data);
            BinaryFormatter binaryFormatter = new();
            FileStream fs = new(path, FileMode.Create);
            binaryFormatter.Serialize(fs, content);
            fs.Close();
        }
        public static GameData LoadData()
        {
            if (!File.Exists(path))
            {
                GameDataLoader.Instance.LoadStatus = 1;
                return null;
            }
            BinaryFormatter binaryFormatter = new();
            FileStream fs = new(path, FileMode.Open);
            string content;
            try
            {
                content = binaryFormatter.Deserialize(fs) as string;
            }
            catch (Exception)
            {
                GameDataLoader.Instance.LoadStatus = 2;
                return null;
            }
            finally
            {
                fs.Close();
            }
            return JsonUtility.FromJson<GameData>(content);
        }
        public static void DeleteData()
        {
            File.Delete(path);
        }
    }
}