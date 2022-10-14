using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Helpers.Config;
using Helpers.Inventory;

using UnityEngine;

public class InventoryDataIO
{
    private const string FileName = "/InventoryData.dat";

    private HelpersConfig _config;

    public InventoryDataIO(HelpersConfig config)
    {
        _config = config;
    }

    public void SaveData(InventoryData inventoryData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream fileStream = File.Create(Application.persistentDataPath + FileName);

        binaryFormatter.Serialize(fileStream, inventoryData);

        fileStream.Close();

        fileStream.Dispose();
    }

    public InventoryData LoadData()
    {
        InventoryData inventoryData;

        FileStream fileStream = File.Open(Application.persistentDataPath + FileName, FileMode.OpenOrCreate);

        if (fileStream.Length == 0)
        {
            inventoryData = new InventoryData(_config);
        }
        else
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            inventoryData = (InventoryData)binaryFormatter.Deserialize(fileStream);
        }

        fileStream.Close();

        fileStream.Dispose();

        return inventoryData;
    }
}
