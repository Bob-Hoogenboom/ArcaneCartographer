using UnityEngine;


public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public GameObject buttonPrefab;
    public Transform buttonParent;


    private void Awake()
    {
        //Singleton Instance
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SaveSystem.Init();
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

        SaveSystem.Save(json);

        Debug.Log($"Saved: {GameManager.Instance.Seed} containing: " +
            $"{GameManager.Instance.Iterations} ," +
            $"{GameManager.Instance.WalkLength} ," +
            $"{GameManager.Instance.RandomStart}");
    }

    public void GetFileFromExplorer()
    {
        string saveString = SaveSystem.Load();

        if (saveString == null) return;

        GameObject buttonObj = Instantiate(buttonPrefab, buttonParent);
        //SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

        buttonObj.GetComponent<SaveFileUI>().savePath = saveString;
    }


    public void LoadFromFile(string jsonPath)
    {
        //TODO
        //check json validate
        //convert jsonpath to save data
        string saveString = jsonPath;

        if(saveString != null)
        {
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
            Debug.Log($"No Save {saveString}");
        }
    }
}
