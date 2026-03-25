using UnityEngine;
using UnityEngine.Networking;

public class DoorController : NetworkBehaviour
{
    private NetworkVariable<bool> doorDestroyed = new NetworkVariable<bool>(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    public override void OnNetworkSpawn()
    {
        doorDestroyed.OnValueChanged += (_, isDestroyed) =>
        {
            if (isDestroyed) gameObject.SetActive(false);
        };
    }

    public void OpenDoor()
    {
        if (IsServer) DestroyDoorServerRpc();
        else RequestDestroyServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestDestroyServerRpc() => DestroyDoorServerRpc();

    [ServerRpc]
    private void DestroyDoorServerRpc()
    {
        doorDestroyed.Value = true;
    }
}
