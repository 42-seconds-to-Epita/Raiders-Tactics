using System.Collections;
using System.Collections.Generic;
using Mirror;
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
    
    
    
    #region Client
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if (!hasAuthority)
        {
            return;
        }
        
        CmdSpawnUnit();

    }
    
    #endregion
}
