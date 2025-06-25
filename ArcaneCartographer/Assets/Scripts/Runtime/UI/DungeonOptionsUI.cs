using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonOptionsUI : MonoBehaviour
{
    //TODO Ask player to save before leaving

    public void BackToMainMenu(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void OnDestroyDecoration()
    {
        foreach(var obj in DecorationManager.Instance.objectsOnGrid.Values)
        {
            if(obj != null) 
            {
                Destroy(obj);
            }
        }

        foreach (var obj in DecorationManager.Instance.currentOBJs)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        DecorationManager.Instance.objectsOnGrid.Clear();
    }


    public void QuitButton()
    {
        Application.Quit();
    }
}
