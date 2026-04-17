using UnityEngine;
using Unity.Netcode;

public class KeyNumbers : NetworkBehaviour
{
    // DO NOT FUCKING CHANGE THIS NUMBER I DID NOT ACCOUNT FOR THAT IN THE OTHER SCRIPT I'LL KILL YOU
    public CodeDisplayNumbers[] numberObjects = new CodeDisplayNumbers[3];
    public Transform[] spawnLocations = new Transform[7];

    private KeypadScript keypad;

    public override void OnNetworkSpawn()
    {
        keypad = FindObjectOfType<KeypadScript>();
        if (keypad == null) return;

        if (IsServer)
        {
            // Pick 3 random spawn indices on the server and send to all clients
            int[] indices = GetShuffledIndices();
            SpawnNumbersClientRpc(indices[0], indices[1], indices[2]);
        }
    }

    [ClientRpc]
    private void SpawnNumbersClientRpc(int i0, int i1, int i2)
    {
        int[] indices = { i0, i1, i2 };
        for (int i = 0; i < 3; i++)
        {
            int digit = keypad.GetCodeDigit(i);
            Transform spawn = spawnLocations[indices[i]];
            numberObjects[i].transform.position = spawn.position;
            numberObjects[i].transform.rotation = spawn.rotation;
            numberObjects[i].SetDigit(digit);
            numberObjects[i].gameObject.SetActive(true);
        }
    }

    private int[] GetShuffledIndices()
    {
        int[] indices = new int[spawnLocations.Length];
        for (int i = 0; i < indices.Length; i++) indices[i] = i;

        for (int i = indices.Length - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            (indices[i], indices[rand]) = (indices[rand], indices[i]);
        }

        return new int[] { indices[0], indices[1], indices[2] };
    }
}