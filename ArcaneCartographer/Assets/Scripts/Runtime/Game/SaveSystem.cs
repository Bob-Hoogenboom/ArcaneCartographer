using System.IO;
using UnityEditor;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/SaveData/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        //open file explorer
        int saveNumber = 1;
        while (File.Exists($"save_{saveNumber}.txt"))
        {
            saveNumber++;
        }

        File.WriteAllText($"{SAVE_FOLDER}save{saveNumber}.txt", saveString);
    }
    public static string Load()
    {
        // open file explorer
        string dataPath = EditorUtility.OpenFilePanel("Open Save File ",  Application.dataPath, "");

        bool jsonOK = IsValidJson(dataPath);
        Debug.Log($"{jsonOK}");

        if (jsonOK)
        {
            string saveString = File.ReadAllText($"{dataPath}");
            return saveString;
        }
        else
        {
            return null;
        }
    }

    public static bool IsValidJson(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogWarning("File path is null or empty.");
            return false;
        }

        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"File does not exist: {filePath}");
            return false;
        }

        try
        {
            string jsonString = File.ReadAllText(filePath); // Read file content as JSON
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(jsonString);

            return saveObject != null;
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"Error reading or parsing JSON: {ex.Message}");
            return false;
        }
    }
}
