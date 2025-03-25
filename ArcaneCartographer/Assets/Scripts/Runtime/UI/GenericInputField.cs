using UnityEngine;
using TMPro;

public class GenericInputField : MonoBehaviour
{
    private TMP_InputField _inputField; 
    public string valueKey;

    public void UpdateGameManagerValue(string newValue)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateValue(valueKey, newValue);
        }
    }

    private void Start()
    {
        if (_inputField == null) _inputField = GetComponent<TMP_InputField>();

        _inputField.onEndEdit.AddListener(UpdateGameManagerValue);
    }

    private void OnDestroy()
    {
        _inputField.onEndEdit.RemoveListener(UpdateGameManagerValue);
    }
}