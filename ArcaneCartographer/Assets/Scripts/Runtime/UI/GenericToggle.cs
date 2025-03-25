using UnityEngine;
using UnityEngine.UI; 

public class GenericToggle : MonoBehaviour
{
    public string valueKey; 
    private Toggle _toggle; 


    private void Start()
    {
        if (_toggle == null) _toggle = GetComponent<Toggle>();

        _toggle.onValueChanged.AddListener(UpdateGameManagerValue);
    }

    private void UpdateGameManagerValue(bool isOn)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateValue(valueKey, isOn.ToString()); // Convert bool to string
        }
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(UpdateGameManagerValue);
    }
}