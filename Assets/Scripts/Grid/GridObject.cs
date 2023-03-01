using UnityEngine;

namespace Grid
{
    public enum PlacesType
    {
        MAIN,
        FLOOR,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    
    public class GridObject
    {
        private Grid<GridObject> grid;
        private int x;
        private int z;

        private PlacedObject placedObject;

        public GridObject(Grid<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetPlacedObject(PlacedObject placedObject)
        {
            this.placedObject = placedObject;
            grid.UpdateDisplay(x, z);
        }

        public PlacedObject GetPlacedObject()
        {
            return placedObject;
        }

        public void ClearPlacedObject()
        {
            placedObject = null;
            grid.UpdateDisplay(x, z);
        }

        public bool CanBuild()
        {
            return placedObject is null;
        }

        public override string ToString()
        {
            return x + ", " + z + "\n" + (placedObject == null ? "0" : "1");
        }
    }
}