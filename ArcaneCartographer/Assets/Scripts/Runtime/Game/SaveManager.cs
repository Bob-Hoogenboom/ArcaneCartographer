using System.Collections;
using System.Collections.Generic;
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
            randomStart = GameManager.Instance.RandomStart,
            placedObjects = DecorationManager.Instance.GetPlacedObjects()
        };

        string json = JsonUtility.ToJson(saveObject, true); // `true` = pretty print
        SaveSystem.Save(json);

        Debug.Log($"Saved: {GameManager.Instance.Seed} containing: " +
            $"{GameManager.Instance.Iterations}, " +
            $"{GameManager.Instance.WalkLength}, " +
            $"{GameManager.Instance.RandomStart}, " +
            $"Objects placed: {saveObject.placedObjects.Count}");
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
        string saveString = jsonPath;

        if (!string.IsNullOrWhiteSpace(saveString))
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            GameManager.Instance.Seed = saveObject.seed;
            GameManager.Instance.Iterations = saveObject.iterations;
            GameManager.Instance.WalkLength = saveObject.walkLength;
            GameManager.Instance.RandomStart = saveObject.randomStart;

            // Generate dungeon here using the values above

            // Load decorations
            GameManager.Instance.objects = saveObject.placedObjects;
        }
        else
        {
            Debug.Log($"No Save {saveString}");
        }
    }
}