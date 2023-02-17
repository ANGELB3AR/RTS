using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : NetworkBehaviour
{
    [SerializeField] Health health = null;

    public static event Action<UnitBase> ServerOnBaseSpawned;
    public static event Action<UnitBase> ServerOnBaseDespawned;
    public static event Action<int> ServerOnPlayerDie;

    #region Server

    public override void OnStartServer()
    {
        ServerOnBaseSpawned?.Invoke(this);
        
        health.ServerOnDie += ServerHandleDie;
    }

    public override void OnStopServer()
    {
        ServerOnBaseDespawned?.Invoke(this);
        
        health.ServerOnDie -= ServerHandleDie;
    }

    [Server]
    void ServerHandleDie()
    {
        ServerOnPlayerDie?.Invoke(connectionToClient.connectionId);

        NetworkServer.Destroy(gameObject);
    }

    #endregion

    #region Client

    #endregion
}
