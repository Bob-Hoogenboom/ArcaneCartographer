using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public static ObjectPlacer Instance;
    private string selectedObjectType = null;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectObjectToPlace(string objectType)
    {
        selectedObjectType = objectType;
        Debug.Log($"Selected object to place: {objectType}");
    }

    public string GetSelectedObjectType()
    {
        return selectedObjectType;
    }
}