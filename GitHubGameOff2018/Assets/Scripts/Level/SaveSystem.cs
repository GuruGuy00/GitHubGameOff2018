using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SaveLevel(LevelData levelData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + levelData.LevelName +".level";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, levelData);
        stream.Close();

    }

    public static LevelData LoadData(string levelName)
    {
        string path = Application.persistentDataPath + "/" + levelName + ".level";
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData levelData = formatter.Deserialize(stream) as LevelData;
            stream.Close();
            return levelData;
        }
        else
        {
            //Debug.LogError("Save File not found" + path);
            LevelData ld = new LevelData(levelName, false, false, false);
            return ld;
        }
    }

    public static void SavePlayerData(PlayerData playerData, string saveSlot)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + "Player" + saveSlot + ".Data";
        Debug.Log(path);

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayerData(string saveSlot)
    {
        string path = Application.persistentDataPath + "/" + "Player" + saveSlot + ".Data";
        
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return playerData;
        }
        else
        {
            Debug.LogError("Player File Not Found @ " + path);
            return null;
        }
    }
}
