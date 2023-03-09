using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public static List<PlacedObject> instances { get; } = new List<PlacedObject>();
    
    public ObjectType objectType { get; private set; }
    public Vector2Int origin { get; private set; }
    public ObjectType.Dir dir { get; private set; }

    public PositionType positionType { get; private set; }
    public int objectTypeId { get; private set; }
    
    public static PlacedObject Create(Vector3 worldPos, Vector2Int origin, ObjectType.Dir dir, ObjectType type, int typeId)
    {
        Transform transform = Instantiate(type.prefab, worldPos, Quaternion.Euler(0, type.GetRotationAngle(dir), 0));

        PlacedObject placedObject = transform.GetComponent<PlacedObject>();

        placedObject.objectType = type;
        placedObject.origin = origin;
        placedObject.dir = dir;
        placedObject.positionType = type.positionType;
        placedObject.objectTypeId = typeId;
        
        instances.Add(placedObject);

        return placedObject;
    }

    public List<Vector2Int> GetGridPositionList()
    {
        return objectType.GetGridPositionList(origin, dir);
    }
    
    public void DestroySelf()
    {
        instances.Remove(this);
        Destroy(gameObject);
    }
}
