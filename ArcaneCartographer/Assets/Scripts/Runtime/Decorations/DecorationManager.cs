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

    private List<PlacableObjectData> placedObjects = new List<PlacableObjectData>();

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
        if (prefabDict.TryGetValue(objectType, out GameObject prefab))
        {
            Instantiate(prefab, position, Quaternion.identity);

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
                Vector3 worldPos = new Vector3(obj.x, 0, obj.y);
                Instantiate(prefab, worldPos, Quaternion.identity);
                placedObjects.Add(obj);
            }
            else
            {
                Debug.LogWarning($"Prefab not found for type '{obj.objectType}'");
            }
        }
    }


    public List<PlacableObjectData> GetPlacedObjects() => placedObjects;
}
