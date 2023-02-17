using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCommandGiver : MonoBehaviour
{
    [SerializeField] UnitSelectionHandler unitSelectionHandler = null;
    [SerializeField] LayerMask layerMask = new LayerMask();

    Camera mainCamera;

    private void OnEnable()
    {
        GameOverHandler.ClientOnGameOver += ClientHandleGameOver;
    }

    private void OnDisable()
    {
        GameOverHandler.ClientOnGameOver -= ClientHandleGameOver;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Mouse.current.rightButton.wasPressedThisFrame) { return; }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) { return; }

        if (hit.collider.TryGetComponent<Targetable>(out Targetable target))
        {
            if (target.isOwned)
            {
                TryMove(hit.point);
                return;
            }

            TryTarget(target);
            return;
        }

        TryMove(hit.point);
    }

    private void TryMove(Vector3 point)
    {
        foreach (Unit unit in unitSelectionHandler.selectedUnits)
        {
            unit.GetUnitMovement().CmdMove(point);
        }
    }

    private void TryTarget(Targetable target)
    {
        foreach (Unit unit in unitSelectionHandler.selectedUnits)
        {
            unit.GetTargeter().CmdSetTarget(target.gameObject);
        }
    }

    void ClientHandleGameOver(string winner)
    {
        enabled = false;
    }
}
