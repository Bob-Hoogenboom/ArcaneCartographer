using System.IO;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private readonly string SAVE_FOLDER = Application.dataPath + "/SaveData/";

    private void Awake()
    {
        //Singleton Instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public void Save()
    {
        SaveObject saveObject = new SaveObject
        {
            seed = GameManager.Instance.Seed,
            iterations = GameManager.Instance.Iterations,
            walkLength = GameManager.Instance.WalkLength,
            randomStart = GameManager.Instance.RandomStart
        };
        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(SAVE_FOLDER + "/save.txt", json);

        Debug.Log($"Saved: {GameManager.Instance.Seed} containing: " +
            $"{GameManager.Instance.Iterations} ," +
            $"{GameManager.Instance.WalkLength} ," +
            $"{GameManager.Instance.RandomStart}");
    }

    public void LoadSaveFile()
    {
        if(File.Exists(SAVE_FOLDER + "/save.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/save.txt");

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            GameManager.Instance.Seed = saveObject.seed;
            GameManager.Instance.Iterations = saveObject.iterations;
            GameManager.Instance.WalkLength = saveObject.walkLength;
            GameManager.Instance.RandomStart = saveObject.randomStart;

            Debug.Log($"Loaded: {saveObject.seed} containing: " +
                $"{saveObject.iterations} ," +
                $"{saveObject.walkLength} ," +
                $"{saveObject.randomStart}");
        }
        else 
        {
            Debug.Log("No Save");
        }
    }
}
