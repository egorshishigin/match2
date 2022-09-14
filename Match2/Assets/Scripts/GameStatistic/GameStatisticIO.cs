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
        GameStatistic gameStatistic;

        FileStream fileStream = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.OpenOrCreate);

        if (fileStream.Length == 0)
        {
            gameStatistic = new GameStatistic();
        }
        else
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            gameStatistic = (GameStatistic)binaryFormatter.Deserialize(fileStream);
        }

        fileStream.Close();

        fileStream.Dispose();

        return gameStatistic;
    }
}
