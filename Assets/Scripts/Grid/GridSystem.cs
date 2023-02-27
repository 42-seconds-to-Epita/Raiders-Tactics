using System.Collections.Generic;
using Mirror;
using QuickStart.Utils;
using UnityEngine;

namespace Grid
{
    public class GridSystem : NetworkBehaviour
    {
        public ObjectType testItem;

        private Grid<GridObject> grid;
        private ObjectType.Dir dir = ObjectType.Dir.Down;

        private void Start()
        {
            grid = new Grid<GridObject>(20, 20, 1.5f, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                grid.GetXY(PlayerInteractUtils.GetMouseWorldPosition(), out int x, out int z);

                List<Vector2Int> gridPositions =
                    testItem.GetGridPositionList(new Vector2Int(x, z), dir);


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
                    Vector2Int rotationOffset = testItem.GetRotationOffset(dir);
                    Vector3 objectWorldPos = grid.GetWorldPositionMiddle(x, z) +
                                             new Vector3(rotationOffset.x, 0, rotationOffset.y) +
                                             new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                    Transform builtTransform = Instantiate(testItem.prefab, objectWorldPos,
                        Quaternion.Euler(0, testItem.GetRotationAngle(dir), 0));

                    foreach (Vector2Int gridPosition in gridPositions)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetMainItemTransform(builtTransform);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                dir = ObjectType.GetNextDir(dir);
            }
        }
    }
}