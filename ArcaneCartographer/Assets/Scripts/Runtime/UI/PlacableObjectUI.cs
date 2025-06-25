using UnityEngine;

public class PlacableObjectUI : MonoBehaviour
{
    [Tooltip("Set the name of the prefab this button is associated to")]
    public void OnObjectButtonClicked(string obj)
    {
        ObjectPlacer.Instance.SelectObjectToPlace(obj);
    }
}
