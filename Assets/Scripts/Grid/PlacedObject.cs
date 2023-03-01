using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    private ObjectType objectType;
    private Vector2Int origin;
    private ObjectType.Dir dir;

    public static PlacedObject Create(Vector3 worldPos, Vector2Int origin, ObjectType.Dir dir, ObjectType type)
    {
        Transform transform = Instantiate(type.prefab, worldPos, Quaternion.Euler(0, type.GetRotationAngle(dir), 0));

        PlacedObject placedObject = transform.GetComponent<PlacedObject>();

        placedObject.objectType = type;
        placedObject.origin = origin;
        placedObject.dir = dir;

        return placedObject;
    }

    public List<Vector2Int> GetGridPositionList()
    {
        return objectType.GetGridPositionList(origin, dir);
    }
    
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
