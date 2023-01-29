using Mirror;
using QuickStart.Grid;
using QuickStart.Utils;
using UnityEngine;

public class Testing : NetworkBehaviour
{
    private Grid<GridObject> grid;

    private void Start()
    {
        grid = new Grid<GridObject>(20, 10, 5f, () => new GridObject(false));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           grid.SetValue(PlayerInteractUtils.GetMouseWorldPosition(), new GridObject(true));
        }
    }
}