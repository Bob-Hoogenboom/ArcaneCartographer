using TMPro;
using UnityEngine;

public class SeedInputField : MonoBehaviour
{
    [SerializeField] 
    private TMP_InputField seedInputField; // Assign in Inspector
    [SerializeField]
    private TMP_Text feedbackText;

    private void GetSeedOnChanged(int newSeed)
    {
        seedInputField.text = newSeed.ToString();
        feedbackText.text = "";
        seedInputField.ForceLabelUpdate();
    }


    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnSeedChanged += GetSeedOnChanged;
            //Set input field to show the current seed at start
            seedInputField.text = GameManager.Instance.Seed.ToString();
        }

        //Subscribe to input field changes
        seedInputField.onEndEdit.AddListener(SetSeedFromInput);
    }

    public void SetSeedFromInput(string input)
    {
        if (int.TryParse(input, out int newSeed))
        {
            GameManager.Instance.Seed = newSeed;
        }
        else
        {
            Debug.LogWarning("Invalid seed input! Please enter a valid number.");
            feedbackText.text = "Invalid Seed, Numbers Only!";
            seedInputField.text = GameManager.Instance.Seed.ToString(); // Reset to current seed
        }
    }

    public void RandomSeedButton()
    {
        GameManager.Instance.GenerateSeed();
    }

    //Prevents memory leaks in EventListeners
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnSeedChanged -= GetSeedOnChanged;
        }
        seedInputField.onEndEdit.RemoveListener(SetSeedFromInput);
    }
}
