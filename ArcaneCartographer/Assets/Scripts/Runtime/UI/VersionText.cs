using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textElement;

    private void Start()
    {
        textElement.text = $"Version: {Application.version} ";
    }
}
