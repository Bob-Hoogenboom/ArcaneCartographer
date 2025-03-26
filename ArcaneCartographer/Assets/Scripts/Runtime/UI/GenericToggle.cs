using UnityEngine;
using UnityEngine.UI; 

public class GenericToggle : MonoBehaviour
{
    public string valueKey; 
    private Toggle _toggle; 


    private void Start()
    {
        if (_toggle == null) _toggle = GetComponent<Toggle>();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnValueSetterChanged += OnValueChanged;
            //Set input field to show the current seed at start
            _toggle.isOn = GameManager.Instance.GetBoolValue(valueKey);
        }

        _toggle.onValueChanged.AddListener(UpdateGameManagerValue);
    }

    private void UpdateGameManagerValue(bool isOn)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateValue(valueKey, isOn.ToString()); // Convert bool to string
        }
    }

    private void OnValueChanged(string value)
    {
        _toggle.isOn = bool.TryParse(value, out bool result);
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(UpdateGameManagerValue);
    }
}