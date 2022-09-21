using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

namespace GameStatistic.IO
{
    public class GameStatisticIO
    {
        private const string FileName = "/GameData.dat";

        public void SaveData(GameStatisticData gameStatistic)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            FileStream fileStream = File.Create(Application.persistentDataPath + FileName);

            binaryFormatter.Serialize(fileStream, gameStatistic);

            fileStream.Close();

            fileStream.Dispose();
        }

        public GameStatisticData LoadData()
        {
            GameStatisticData gameStatistic;

            FileStream fileStream = File.Open(Application.persistentDataPath + FileName, FileMode.OpenOrCreate);

            if (fileStream.Length == 0)
            {
                gameStatistic = new GameStatisticData();
            }
            else
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                gameStatistic = (GameStatisticData)binaryFormatter.Deserialize(fileStream);
            }

            fileStream.Close();

            fileStream.Dispose();

            return gameStatistic;
        }
    }
}