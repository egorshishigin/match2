using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

public class GameStatisticIO
{
    public void SaveData(GameStatistic gameStatistic)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream fileStream = File.Create(Application.persistentDataPath + "/GameData.dat");

        binaryFormatter.Serialize(fileStream, gameStatistic);

        fileStream.Close();

        fileStream.Dispose();
    }

    public GameStatistic LoadData()
    {
        try
        {
            FileStream fileStream = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            GameStatistic gameStatistic = (GameStatistic)binaryFormatter.Deserialize(fileStream);

            fileStream.Close();

            fileStream.Dispose();

            return gameStatistic;
        }
        catch (Exception)
        {
            GameStatistic gameStatistic = NewFile();

            throw new Exception("Can't load game data. File doesn't exist. New file been created.");
        }
    }

    private GameStatistic NewFile()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream fileStream = File.Create(Application.persistentDataPath + "/GameData.dat");

        GameStatistic gameStatistic = new GameStatistic();

        binaryFormatter.Serialize(fileStream, gameStatistic);

        fileStream.Close();

        fileStream.Dispose();

        return gameStatistic;
    }
}
