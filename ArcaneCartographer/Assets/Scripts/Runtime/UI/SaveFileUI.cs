using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveFileUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text fileName;
    private string _name;
    private string _savePath;
    public string savePath
    {
        get => _savePath;
        set
        {
            if (_savePath != value)
            {
                _savePath = value;
                SaveUIChanged();
            }
        }
    }
    
    //TODO  no jsonpath == no loading game


    public void SaveUIChanged()
    {
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(_savePath);

        _name = saveObject.seed.ToString();
        fileName.text = _name;
    }

    public void ToEditorScene(int sceneIndex)
    {
        GameManager.Instance.savePath = _savePath;
        SceneManager.LoadScene(sceneIndex);
    }
}
