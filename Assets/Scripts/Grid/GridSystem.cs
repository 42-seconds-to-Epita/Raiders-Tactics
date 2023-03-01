using System;
using System.Collections.Generic;
using Mirror;
using QuickStart.Utils;
using UnityEngine;

namespace Grid
{
    public class GridSystem : NetworkBehaviour
    {
        public static GridSystem Instance { get; private set; }

        public event EventHandler OnSelectedChanged;
        public event EventHandler OnObjectPlaced;

        public List<ObjectType> testItemList;
        private int selectedItem = 0;

        private Grid<GridObject> grid;
        private ObjectType.Dir dir = ObjectType.Dir.Down;

        private void Start()
        {
            Instance = this;
            grid = new Grid<GridObject>(20, 20, 1.5f, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                grid.GetXY(PlayerInteractUtils.GetMouseWorldPosition(), out int x, out int z);

                List<Vector2Int> gridPositions =
                    testItemList[selectedItem].GetGridPositionList(new Vector2Int(x, z), dir);


                bool canBuild = true;
                foreach (Vector2Int gridPosition in gridPositions)
                {
                    if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                    {
                        canBuild = false;
                        break;
                    }
                }

                if (canBuild)
                {
                    Vector2Int rotationOffset = testItemList[selectedItem].GetRotationOffset(dir);
                    Vector3 objectWorldPos = grid.GetWorldPosition(x, z) +
                                             new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                    PlacedObject placedObject = PlacedObject.Create(objectWorldPos, new Vector2Int(x, z), dir,
                        testItemList[selectedItem]);

                    OnObjectPlaced?.Invoke(this, EventArgs.Empty);

                    foreach (Vector2Int gridPosition in gridPositions)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                grid.GetXY(PlayerInteractUtils.GetMouseWorldPosition(), out int x, out int z);
                GridObject gridObject = grid.GetGridObject(x, z);

                PlacedObject placedObject = gridObject.GetPlacedObject();

                if (placedObject is not null)
                {
                    placedObject.DestroySelf();

                    List<Vector2Int> gridPositions = placedObject.GetGridPositionList();

                    foreach (Vector2Int gridPosition in gridPositions)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                dir = ObjectType.GetNextDir(dir);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                selectedItem += 1;
                selectedItem %= testItemList.Count;
                RefreshSelectedObjectType();
            }
        }

        private void RefreshSelectedObjectType()
        {
            OnSelectedChanged?.Invoke(this, EventArgs.Empty);
        }

        public Grid<GridObject> getGrid()
        {
            return grid;
        }

        public Vector3 GetMouseWorldSnappedPosition()
        {
            Vector3 mousePosition = PlayerInteractUtils.GetMouseWorldPosition();
            grid.GetXY(mousePosition, out int x, out int y);

            Vector2Int rotationOffset = testItemList[selectedItem].GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, y) +
                                                new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        }

        public Quaternion GetPlacedObjectRotation()
        {
            return Quaternion.Euler(0, testItemList[selectedItem].GetRotationAngle(dir), 0);
        }

        public ObjectType GetSelected()
        {
            return testItemList[selectedItem];
        }
    }
}