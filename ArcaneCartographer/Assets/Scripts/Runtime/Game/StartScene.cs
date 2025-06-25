using UnityEngine;

public class StartScene : MonoBehaviour
{
    private void Start()
    {
        SaveManager.Instance.LoadFromFile(GameManager.Instance.savePath);
    }
}
