using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

public class GameStatisticIO
{
    public void SaveData(GameStatisticModel gameStatistic)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream fileStream = File.Create(Application.persistentDataPath + "/GameData.dat");

        binaryFormatter.Serialize(fileStream, gameStatistic);

        fileStream.Close();

        fileStream.Dispose();
    }

    public GameStatisticModel LoadData()
    {
        try
        {
            FileStream fileStream = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            GameStatisticModel gameStatistic = (GameStatisticModel)binaryFormatter.Deserialize(fileStream);

            fileStream.Close();

            fileStream.Dispose();

            return gameStatistic;
        }
        catch (Exception)
        {
            throw new Exception("Can't load game data. File doesn't exist.");
        }
    }
}
