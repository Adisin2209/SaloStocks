using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
  public static void SaveData()
  {
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/SaveData.salo";
    FileStream stream = new FileStream(path, FileMode.Create);
    
    SaveData data = new SaveData();
    
    formatter.Serialize(stream, data);
    stream.Close();
  }


  public static SaveData LoadData()
  {
    string path = Application.persistentDataPath + "/SaveData.salo";
    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Open);
      SaveData data = formatter.Deserialize(stream) as SaveData;
      stream.Close();
      return data;
    }
    else
    {
      Debug.Log("No save data found");
      return null;
    }
  }
}
