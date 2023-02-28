using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NewNetwork : NetworkManager
{

    [SerializeField] private GameObject unitSpawnerPrefab = null;

    // Start is called before the first frame update
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        GameObject unitSpawnerInstance = Instantiate(unitSpawnerPrefab, Vector3.zero, 
            Quaternion.identity);
        NetworkServer.Spawn(unitSpawnerInstance, conn);
    }
}