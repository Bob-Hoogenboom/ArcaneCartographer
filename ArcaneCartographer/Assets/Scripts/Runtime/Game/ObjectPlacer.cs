using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public static ObjectPlacer Instance;
    private string selectedObjectType = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Right-click to remove
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                
                DungeonTile tile = hitObject.GetComponent<DungeonTile>();
                if (tile != null || hitObject.layer == LayerMask.NameToLayer("Placable"))
                {
                    Vector3 position = hitObject.transform.position;
                    string selected = GetSelectedObjectType();
                    DecorationManager.Instance.PlaceObject(selected, tile.gridPos.x, tile.gridPos.y, position);
                }
                if(hitObject.layer == LayerMask.NameToLayer("Decoration"))
                {

                    DecorationManager.Instance.RemovePlacedObject(hitObject);
                }
            }
        }
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