using System;
using QuickStart.Utils;
using UnityEngine;

public class Grid<TGridObject>
{

    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;
    
    public Grid(int width, int height, float cellSize, Func<TGridObject> createGridObject)
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
                gridArray[x, y] = createGridObject();
            }
        }

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x, y] = WorldUtils.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, 0, cellSize) * 0.5f, 10, Color.red,
                    TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.black, 100f);
            }
        }
        
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * cellSize;
    }

    private (int x, int y) GetXY(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.z / cellSize);
        return (x, y);
    }

    private void SetValue(int x, int y, TGridObject value)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return;
        }

        gridArray[x, y] = value;
        debugTextArray[x, y].text = value.ToString();
    }

    public void SetValue(Vector3 worldPosition, TGridObject value)
    {
        (int x, int y) pos = GetXY(worldPosition);
        SetValue(pos.x, pos.y, value);
    }

    public TGridObject GetValue(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return default(TGridObject);
        }

        return gridArray[x, y];
    }
}
