using UnityEngine;

namespace Grid
{
    public class SaveObject
    {
        public int objectTypeId;
        public Vector2Int origin;
        public string dir;
        public string positionType;

        public SaveObject(int objectTypeId, Vector2Int origin, string dir, string positionType)
        {
            this.objectTypeId = objectTypeId;
            this.origin = origin;
            this.dir = dir;
            this.positionType = positionType;
        }
    }
}