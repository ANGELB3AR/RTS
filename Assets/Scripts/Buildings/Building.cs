using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : NetworkBehaviour
{
    [SerializeField] GameObject buildingPreview = null;
    [SerializeField] Sprite icon = null;
    [SerializeField] int id = -1;
    [SerializeField] int price = 100;

    public static event Action<Building> ServerOnBuildingSpawn;
    public static event Action<Building> ServerOnBuildingDespawn;

    public static event Action<Building> AuthorityOnBuildingSpawned;
    public static event Action<Building> AuthorityOnBuildingDespawned;

    public GameObject GetBuildingPreview()
    {
        return buildingPreview;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public int GetId()
    {
        return id;
    }

    public int GetPrice()
    {
        return price;
    }

    #region Server

    public override void OnStartServer()
    {
        ServerOnBuildingSpawn?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnBuildingDespawn?.Invoke(this);
    }

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        AuthorityOnBuildingSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        if (!isOwned) { return; }

        AuthorityOnBuildingDespawned?.Invoke(this);
    }

    #endregion
}
