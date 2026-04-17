using UnityEngine;
using Unity.Netcode;

public class KeyButtons : NetworkBehaviour
{
    public int keyNumber;

    private KeypadScript keypad;

    private void Start()
    {
        keypad = GetComponentInParent<KeypadScript>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            PressKeyServerRpc(keyNumber);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void PressKeyServerRpc(int number)
    {
        PressKeyClientRpc(number);
    }

    [ClientRpc]
    private void PressKeyClientRpc(int number)
    {
        keypad.PressKey(number);
    }
}