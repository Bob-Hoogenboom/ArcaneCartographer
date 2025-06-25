using UnityEngine;

public class StartScene : MonoBehaviour
{
    private void Start()
    {
        if(!string.IsNullOrWhiteSpace(GameManager.Instance.savePath))
        {
            SaveManager.Instance.LoadFromFile(GameManager.Instance.savePath);
        }
    }
}
