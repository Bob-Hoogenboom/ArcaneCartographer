using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveFileUI : MonoBehaviour
{
    public void ToEditorScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
