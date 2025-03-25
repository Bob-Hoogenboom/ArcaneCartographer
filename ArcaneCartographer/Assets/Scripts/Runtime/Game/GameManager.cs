using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dungeon Variables")]
    [SerializeField]
    private int _seed;
    [Space]
    public int iterations = 10;
    public int walkLength = 10;
    public bool randomStart = true;

    //Actions:
    public event Action<int> OnSeedChanged;
    public Dictionary<string, Action<string>> valueSetters;

    public int Seed
    {
        get => _seed;
        set
        {
            if (_seed != value) // Only trigger event if value actually changes
            {
                _seed = value;
                OnSeedChanged?.Invoke(_seed);
            }
        }
    }

    //Singleton GameManager
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        valueSetters = new Dictionary<string, Action<string>>()
        {
            {"iterations", value => int.TryParse (value, out iterations )},
            {"walkLength", value => int.TryParse (value, out walkLength )},
            {"randomStart", value => bool.TryParse (value, out randomStart )},
        };
    }

    public void GenerateSeed()
    {
        Seed = UnityEngine.Random.Range(0, int.MaxValue);
    }

    // Generic method to update any variable dynamically
    public void UpdateValue(string key, string newValue)
    {
        if (valueSetters.TryGetValue(key, out var setter))
        {
            setter.Invoke(newValue);
            //Debug.Log($"Updated {key} to {newValue}");
        }
        else
        {
            Debug.LogWarning($"No variable found for key: {key}");
        }
    }
}
