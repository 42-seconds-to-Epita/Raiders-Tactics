using System.Collections.Generic;
using System.Linq;

namespace Grid
{
    public enum PositionType
    {
        MAIN,
        FLOOR,
        WALL,
        
        //Not used in scriptable object
        WALL_UP,
        WALL_DOWN,
        WALL_LEFT,
        WALL_RIGHT
    }
    
    public class GridObject
    {
        private Grid<GridObject> grid;
        private int x;
        private int z;

        private Dictionary<PositionType, PlacedObject> placedObjectMap;

        public GridObject(Grid<GridObject> grid, int x, int z)
        {
            placedObjectMap = new Dictionary<PositionType, PlacedObject>();
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetPlacedObject(PlacedObject placedObject, PositionType type)
        {
            if (placedObjectMap.ContainsKey(type))
            {
                placedObjectMap[type] = placedObject;
            }
            else
            {
                placedObjectMap.Add(type, placedObject);
            }
            
            grid.UpdateDisplay(x, z);
        }

        public PlacedObject GetPlacedObject(PositionType type)
        {
            if (placedObjectMap.ContainsKey(type))
            {
                return placedObjectMap[type];
            }

            return null;
        }

        public List<PlacedObject> GetAllPlacedObject()
        {
            return placedObjectMap.Values.ToList();
        }

        public void ClearPlacedObject(PositionType type)
        {
            if (type == PositionType.WALL)
            {
                placedObjectMap.Remove(PositionType.WALL_UP);
                placedObjectMap.Remove(PositionType.WALL_DOWN);
                placedObjectMap.Remove(PositionType.WALL_LEFT);
                placedObjectMap.Remove(PositionType.WALL_RIGHT);
                grid.UpdateDisplay(x, z);
                return;
            }
            
            if (!placedObjectMap.ContainsKey(type))
            {
                return;
            }
            
            placedObjectMap[type] = null;
            grid.UpdateDisplay(x, z);
        }
        
        public bool CanBuild(PositionType type)
        {
            return !placedObjectMap.ContainsKey(type) || placedObjectMap[type] is null;
        }

        public override string ToString()
        {
            return x + ", " + z + "\n" + (placedObjectMap.Count == 0 ? "0" : "1");
        }
    }
}