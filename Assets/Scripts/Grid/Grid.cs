using System;
using QuickStart.Utils;
using UnityEngine;

namespace Grid
{
    public class Grid<TGridObject>
    {
        private int width;
        private int height;
        private float cellSize;
        private TGridObject[,] gridArray;
        private TextMesh[,] debugTextArray;

        public Grid(int width, int height, float cellSize,
            Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            gridArray = new TGridObject[width, height];
            debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    gridArray[x, y] = createGridObject(this, x, y);
                }
            }


            bool debug = false;
            if (debug)
            {
                for (int x = 0; x < gridArray.GetLength(0); x++)
                {
                    for (int y = 0; y < gridArray.GetLength(1); y++)
                    {
                        debugTextArray[x, y] = WorldUtils.CreateWorldText(gridArray[x, y].ToString(), null,
                            GetWorldPosition(x, y) + new Vector3(cellSize, 0, cellSize) * 0.5f, 5, Color.red,
                            TextAnchor.MiddleCenter);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.black, 100f);
                    }
                }

                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);
            }
        }

        public void UpdateDisplay(int x, int z)
        {
            if (false && debugTextArray !=  null)
            {
                debugTextArray[x, z].text = gridArray[x, z].ToString();
            }
        }

        public Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x, 0, z) * cellSize;
        }

        public Vector3 GetWorldPositionMiddle(int x, int z)
        {
            return new Vector3(x, 0, z) * cellSize + new Vector3(cellSize / 2, 0, cellSize / 2);
        }

        public (int x, int z) GetXY(Vector3 worldPosition)
        {
            int x = Mathf.FloorToInt(worldPosition.x / cellSize);
            int z = Mathf.FloorToInt(worldPosition.z / cellSize);
            return (x, z);
        }

        public void GetXY(Vector3 worldPosition, out int x, out int z)
        {
            x = Mathf.FloorToInt(worldPosition.x / cellSize);
            z = Mathf.FloorToInt(worldPosition.z / cellSize);
        }

        private void SetValue(int x, int z, TGridObject value)
        {
            if (x < 0 || z < 0 || x >= width || z >= height)
            {
                return;
            }

            gridArray[x, z] = value;
            debugTextArray[x, z].text = value.ToString();
        }

        public void SetValue(Vector3 worldPosition, TGridObject value)
        {
            (int x, int z) pos = GetXY(worldPosition);
            SetValue(pos.x, pos.z, value);
        }

        public TGridObject GetGridObject(int x, int z)
        {
            if (x < 0 || z < 0 || x >= width || z >= height)
            {
                return default(TGridObject);
            }

            return gridArray[x, z];
        }

        public float GetCellSize()
        {
            return cellSize;
        }
    }
}