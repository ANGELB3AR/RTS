using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class ResourcesDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resourcesText = null;

    RTSPlayer player;

    private void Start()
    {
        player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
    }

    private void Update()
    {
        if (player!= null)
        {
            ClientHandleResourcesUpdated(player.GetResources());

            player.ClientOnResourcesUpdated += ClientHandleResourcesUpdated;
        }
    }

    private void OnDisable()
    {
        player.ClientOnResourcesUpdated -= ClientHandleResourcesUpdated;
    }

    void ClientHandleResourcesUpdated(int resources)
    {
        resourcesText.text = $"Resources: {resources}";
    }
}
