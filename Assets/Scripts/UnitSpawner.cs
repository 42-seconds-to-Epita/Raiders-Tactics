using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using QuickStart.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawner : NetworkBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject unitPrefab = null;
    [SerializeField] private Transform SpawnPoint = null;
    
    #region Server

    [Command]
    private void CmdSpawnUnit()
    {
        GameObject unitSpawn = Instantiate(unitPrefab, SpawnPoint.position, SpawnPoint.rotation);
        
        NetworkServer.Spawn(unitSpawn,connectionToClient);
    }
    
    #endregion

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = PlayerInteractUtils.GetMouseWorldPosition();
            CmdSpawnUnit();
        }
    }


    #region Client
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("1");
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        Debug.Log("2");

        if (!hasAuthority)
        {
            return;
        }
        
        Debug.Log("3");
        
        CmdSpawnUnit();

    }
    
    #endregion
}
