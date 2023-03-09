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

        public bool isSoutenance;

        private void Start()
        {
            Instance = this;
            grid = new Grid<GridObject>(20, 20, 1.5f, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
            
            string[] result = SaveUtils.Load("test").Split("\n");
            List<SaveObject> toAdd = new List<SaveObject>();
            
            foreach (string s in result)
            {
                toAdd.Add(JsonUtility.FromJson<SaveObject>(s));
            }
            
            LoadAllObjects(toAdd);
        }

        private void Update()
        {
            if (isSoutenance)
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                grid.GetXY(PlayerInteractUtils.GetMouseWorldPosition(), out int x, out int z);

                List<Vector2Int> gridPositions =
                    testItemList[selectedItem].GetGridPositionList(new Vector2Int(x, z), dir);


                bool canBuild = true;
                foreach (Vector2Int gridPosition in gridPositions)
                {
                    if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild(getSelectedPositionType()))
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
                        testItemList[selectedItem], selectedItem);

                    OnObjectPlaced?.Invoke(this, EventArgs.Empty);

                    foreach (Vector2Int gridPosition in gridPositions)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y)
                            .SetPlacedObject(placedObject, getSelectedPositionType());
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                grid.GetXY(PlayerInteractUtils.GetMouseWorldPosition(), out int x, out int z);
                GridObject gridObject = grid.GetGridObject(x, z);

                foreach (PlacedObject placedObject in gridObject.GetAllPlacedObject())
                {
                    if (placedObject is null)
                    {
                        continue;
                    }

                    placedObject.DestroySelf();

                    List<Vector2Int> gridPositions = placedObject.GetGridPositionList();

                    foreach (Vector2Int gridPosition in gridPositions)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject(placedObject.positionType);
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
            if (isSoutenance)
            {
                return;
            }
            OnSelectedChanged?.Invoke(this, EventArgs.Empty);
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
            return Quaternion.Euler(0, GetSelected().GetRotationAngle(dir), 0);
        }

        public ObjectType GetSelected()
        {
            return testItemList[selectedItem];
        }

        public PositionType getSelectedPositionType()
        {
            if (GetSelected().positionType != PositionType.WALL)
            {
                return GetSelected().positionType;
            }

            switch (dir)
            {
                default:
                case ObjectType.Dir.Down: return PositionType.WALL_DOWN;
                case ObjectType.Dir.Left: return PositionType.WALL_LEFT;
                case ObjectType.Dir.Up: return PositionType.WALL_UP;
                case ObjectType.Dir.Right: return PositionType.WALL_RIGHT;
            }
        }

        public PositionType PosTypeByName(string name)
        {
            switch (name)
            {
                default:
                case "MAIN": return PositionType.MAIN;
                case "FLOOR": return PositionType.FLOOR;
                case "WALL": return PositionType.WALL;
                case "WALL_UP": return PositionType.WALL_UP;
                case "WALL_DOWN": return PositionType.WALL_DOWN;
                case "WALL_LEFT": return PositionType.WALL_LEFT;
                case "WALL_RIGHT": return PositionType.WALL_RIGHT;
            }
        }

        public void LoadAllObjects(List<SaveObject> toLoad)
        {
            int i = 0;
            foreach (SaveObject saveObject in toLoad)
            {
                i += 1;
                Debug.Log(i);
                if (selectedItem >= testItemList.Count || selectedItem < 0 || testItemList[selectedItem] == null)
                {
                    Debug.Log("WTF load all objects");
                    continue;
                }
                
                List<Vector2Int> gridPositions =
                    testItemList[selectedItem]
                        .GetGridPositionList(new Vector2Int(saveObject.origin.x, saveObject.origin.y),
                            ObjectType.dirByName(saveObject.dir));

                
                Vector2Int rotationOffset = testItemList[saveObject.objectTypeId]
                    .GetRotationOffset(ObjectType.dirByName(saveObject.dir));
                Vector3 objectWorldPos = grid.GetWorldPosition(saveObject.origin.x, saveObject.origin.y) +
                                         new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                PlacedObject placedObject = PlacedObject.Create(objectWorldPos,
                    new Vector2Int(saveObject.origin.x, saveObject.origin.y), ObjectType.dirByName(saveObject.dir),
                    testItemList[saveObject.objectTypeId], saveObject.objectTypeId);

                OnObjectPlaced?.Invoke(this, EventArgs.Empty);

                foreach (Vector2Int gridPosition in gridPositions)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y)
                        .SetPlacedObject(placedObject, PosTypeByName(saveObject.positionType));
                }
            }
        }
    }
}