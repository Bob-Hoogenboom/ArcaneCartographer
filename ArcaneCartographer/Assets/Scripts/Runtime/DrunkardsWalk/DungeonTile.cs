using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTile : MonoBehaviour
{
    public Vector2Int gridPos;


/*    private void OnMouseDown()
    {
        string selected = ObjectPlacer.Instance.GetSelectedObjectType();
        if (string.IsNullOrEmpty(selected))
        {
            Debug.Log("No object selected to place.");
            return;
        }

        Vector3 position = transform.position;
        DecorationManager.Instance.PlaceObject(selected, gridPos.x, gridPos.y, position);
    }*/
}
