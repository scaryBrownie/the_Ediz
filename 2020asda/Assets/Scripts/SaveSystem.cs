using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{

    public static void SaveMC(mcScript mc)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/mc.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        MCSaveData data = new MCSaveData(mc);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static MCSaveData LoadMC()
    {
        string path = Application.persistentDataPath + "/mc.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MCSaveData data = formatter.Deserialize(stream) as MCSaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError(path + "yolunda save dosyası bulunamadı.");
            return null;
        }

    }

}
