using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProjectile : NetworkBehaviour
{
    [SerializeField] Rigidbody rb = null;
    [SerializeField] float launchForce = 10f;
    [SerializeField] float destroyAfterSeconds = 5f;

    private void Start()
    {
        rb.velocity = transform.forward * launchForce;
    }

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfterSeconds);
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
}
