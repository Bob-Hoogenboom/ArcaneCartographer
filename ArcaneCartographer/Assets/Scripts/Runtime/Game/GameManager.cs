using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Seed 
    public event Action<int> OnSeedChanged;
    private int _seed;
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
    public static GameManager Instance { get;  private set; }

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
    }

    public void GenerateSeed()
    {
        Seed = UnityEngine.Random.Range(0, int.MaxValue);
    }
}
