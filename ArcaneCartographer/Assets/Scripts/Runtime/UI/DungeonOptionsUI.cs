using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonOptionsUI : MonoBehaviour
{
    //TODO Ask player to save before leaving

    public void BackToMainMenu(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


    public void QuitButton()
    {
        Application.Quit();
    }
}
