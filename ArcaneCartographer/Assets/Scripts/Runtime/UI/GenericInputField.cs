using UnityEngine;
using TMPro;

public class GenericInputField : MonoBehaviour
{
    private TMP_InputField _inputField; 
    public string valueKey;

    private void Start()
    {
        if (_inputField == null) _inputField = GetComponent<TMP_InputField>();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnValueSetterChanged += OnValueChanged;
            //Set input field to show the current seed at start
            _inputField.text = GameManager.Instance.GetValue(valueKey);
        }

        _inputField.onEndEdit.AddListener(UpdateGameManagerValue);
    }

    public void UpdateGameManagerValue(string newValue)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateValue(valueKey, newValue);
        }
    }

    private void OnValueChanged(string value)
    {
        _inputField.text = value;
        //feedbackText.text = "";               //add feedback if the value is not a number*
        _inputField.ForceLabelUpdate();
    }

    private void OnDestroy()
    {
        _inputField.onEndEdit.RemoveListener(UpdateGameManagerValue);
    }
}