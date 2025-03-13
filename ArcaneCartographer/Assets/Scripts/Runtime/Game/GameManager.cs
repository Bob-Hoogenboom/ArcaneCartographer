using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _currentSeed;

    private void Awake()
    {
        //For Debugging
        GenerateSeed();

        SaveObject saveObject = new SaveObject
        {
            seed = 0
        };

        string json = JsonUtility.ToJson(saveObject);
        Debug.Log(json);

        SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(json);
        Debug.Log(loadedSaveObject.seed);
    }


    private void GenerateSeed()
    {
        _currentSeed = UnityEngine.Random.Range(0, int.MaxValue);
    }

    private void Save()
    {
        SaveObject saveObject = new SaveObject
        {
            seed = _currentSeed
        };
        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }
}
