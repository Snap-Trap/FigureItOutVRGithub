using UnityEngine;
using Unity.Netcode;
using Unity.Services.Matchmaker.Models;

public class DoorController : NetworkBehaviour
{
    private NetworkVariable<bool> doorOpened = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        doorOpened.OnValueChanged += OnDoorStateChanged;

        if (doorOpened.Value)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDoorStateChanged(bool previous, bool current)
    {
        if (current)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenDoor()
    {
        if (IsServer) SetDoorOpenedServerRpc();
        else RequestOpenServerRpc();
    }

    [ServerRpc]
    private void RequestOpenServerRpc() => SetDoorOpenedServerRpc();
    [ServerRpc]
    private void SetDoorOpenedServerRpc()
    {
        doorOpened.Value = true;
    }
    public override void OnDestroy()
    {
        doorOpened.OnValueChanged -= OnDoorStateChanged;
    }
}
