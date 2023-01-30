using UnityEngine;

namespace Grid
{
    public class GridObject
    {
        private Grid<GridObject> grid;
        private int x;
        private int z;

        private Transform mainItemTransform;

        public GridObject(Grid<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetMainItemTransform(Transform transform)
        {
            mainItemTransform = transform;
            grid.UpdateDisplay(x, z);
        }

        public void ClearMainItemTransform()
        {
            mainItemTransform = null;
            grid.UpdateDisplay(x, z);
        }

        public bool CanBuild()
        {
            return mainItemTransform is null;
        }

        public override string ToString()
        {
            return x + ", " + z + "\n" + (mainItemTransform == null ? "0" : "1");
        }
    }
}