using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("SaveFile")]
    public string savePath;

    [Header("Dungeon Variables")]
    [SerializeField]
    private int _seed;
    [Space]
    [SerializeField]
    public int _iterations = 10;
    [SerializeField]
    public int _walkLength = 10;
    [SerializeField]
    public bool _randomStart = true;

    //[Header("Delegate Actions")]
    public event Action<int> OnSeedChanged;
    public event Action<string> OnValueSetterChanged;
    public Dictionary<string, Action<string>> valueSetters; //an Action performs and operation but does NOT return anything but takes in a variable*
    private Dictionary<string, Func<string>> valueGetters; //a Func uses methods to compute and return something*

    public List<PlacableObjectData> objects;

    //[Header("Getters and Setters")]
    #region Setters and getters
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

    public int Iterations
    {
        get => _iterations;
        set
        {
            if(_iterations != value)
            {
                _iterations = value;
                OnValueSetterChanged?.Invoke(_iterations.ToString());
            }
        }
    }

    public int WalkLength
    {
        get => _walkLength;
        set
        {
            if (_walkLength != value)
            {
                _walkLength = value;
                OnValueSetterChanged?.Invoke(_walkLength.ToString());
            }
        }
    }

    public bool RandomStart
    {
        get => _randomStart;
        set
        {
            if(_randomStart != value)
            {
                _randomStart = value;
                OnValueSetterChanged?.Invoke(_randomStart.ToString());
            }
        }
    }

    #endregion

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
            {"iterations", value => int.TryParse (value, out _iterations )},
            {"walkLength", value => int.TryParse (value, out _walkLength )},
            {"randomStart", value => bool.TryParse (value, out _randomStart )},
        };

        valueGetters = new Dictionary<string, Func<string>>()
        {
            { "iterations", () => _iterations.ToString() },
            { "walkLength", () => _walkLength.ToString() },
            { "randomStart", () => _randomStart.ToString() }
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

    public string GetValue(string key)
    {
        if (valueGetters.TryGetValue(key, out var getter))
        {
            return getter(); // weird getter? [system.string]?
        }
        return "";
    }

    public bool GetBoolValue(string key)
    {
        return bool.TryParse(GetValue(key), out bool result) && result;
    }
}
