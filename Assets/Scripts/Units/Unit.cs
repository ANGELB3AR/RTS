using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField] UnityEvent onSelected = null;
    [SerializeField] UnityEvent onDeselected = null;

    #region Client

    [Client]
    public void Select()
    {
        if (!isOwned) { return; }

        onSelected?.Invoke();
    }

    [Client]
    public void Deselect()
    {
        if (!isOwned) { return; }

        onDeselected?.Invoke();
    }

    #endregion
}
