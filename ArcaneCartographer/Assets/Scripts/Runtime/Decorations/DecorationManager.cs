using System.Collections.Generic;
using UnityEngine;

public class DecorationManager : MonoBehaviour
{
    public static DecorationManager Instance;

    [System.Serializable]
    public class ObjectTypeMapping
    {
        public string typeName;
        public GameObject prefab;
    }

    public List<ObjectTypeMapping> objectMappings;
    private Dictionary<string, GameObject> prefabDict;

    public List<PlacableObjectData> placedObjects = new List<PlacableObjectData>();
    public Dictionary<Vector2Int, GameObject> objectsOnGrid = new();
    public List<GameObject> currentOBJs = new List<GameObject> ();


    private void Awake()
    {
        Instance = this;

        prefabDict = new Dictionary<string, GameObject>();

        foreach (var mapping in objectMappings)
        {
            if (!prefabDict.ContainsKey(mapping.typeName) && mapping.prefab != null)
            {
                prefabDict[mapping.typeName] = mapping.prefab;
            }
        }
    }

    public void PlaceObject(string objectType, int x, int y, Vector3 position)
    {
        if (objectType == null) return;

        if (prefabDict.TryGetValue(objectType, out GameObject prefab))
        {
            var obj = Instantiate(prefab, position, Quaternion.identity);
            currentOBJs.Add(obj);

            placedObjects.Add(new PlacableObjectData
            {
                objectType = objectType,
                x = x,
                y = y
            });

        }
    }

    public void LoadPlacedObjects(List<PlacableObjectData> objects)
    {
        // Initialize if not already
        if (prefabDict == null)
        {
            Debug.LogWarning("PrefabDict was null — initializing now.");
            prefabDict = new Dictionary<string, GameObject>();

            foreach (var mapping in objectMappings)
            {
                if (!prefabDict.ContainsKey(mapping.typeName) && mapping.prefab != null)
                {
                    prefabDict[mapping.typeName] = mapping.prefab;
                }
            }
        }

        placedObjects.Clear();

        foreach (var obj in objects)
        {
            if (prefabDict.TryGetValue(obj.objectType, out GameObject prefab))
            {
                Vector2Int gridPos = new Vector2Int(obj.x, obj.y);
                Vector3 worldPos = new Vector3(obj.x, 0, obj.y);

                GameObject decorationOBJ = Instantiate(prefab, worldPos, Quaternion.identity);

                objectsOnGrid[gridPos] = decorationOBJ;
                placedObjects.Add(obj);
            }
            else
            {
                Debug.LogWarning($"Prefab not found for type '{obj.objectType}'");
            }
        }
    }

    public void RemovePlacedObject(GameObject target)
    {
        Vector3 pos = target.transform.position;
        Vector2Int gridPos = new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z));

        // Remove from scene
        Destroy(target);

        // Remove from dictionary
        if (objectsOnGrid.ContainsKey(gridPos))
            objectsOnGrid.Remove(gridPos);

        // Remove from saved data
        placedObjects.RemoveAll(p => p.x == gridPos.x && p.y == gridPos.y);

        Debug.Log($"Removed object at {gridPos}");
    }

    public List<PlacableObjectData> GetPlacedObjects() => placedObjects;
}
