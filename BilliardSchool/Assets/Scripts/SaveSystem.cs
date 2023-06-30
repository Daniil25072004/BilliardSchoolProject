using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
   public static void SaveData() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.txt";
        FileStream stream = new FileStream(path, FileMode.Create);
        Data data = new Data();
        formatter.Serialize(stream, data);
        Debug.Log("Data wurde gesaved");
        stream.Close();
   }
   public static Data LoadData() {
        string path = Application.persistentDataPath + "/data.txt";
        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();
            return data;
        } else {
           Debug.LogError("Save file not found in "+ path);
            return null;
        }
   }
}
