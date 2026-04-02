using UnityEngine;
using Unity.Netcode;

public class PressurePlateScript : NetworkBehaviour
{
    public GameObject door;
    private Transform doorOriginalTransform;

    private bool doorIsAlreadyOpen;

    public float weightThreshold = 15f;
    public float weightCurrentTotal;

    public void Start()
    {
        doorOriginalTransform = door.transform;
        weightCurrentTotal = 0f;
    }

    public void Update()
    {
        if (weightThreshold >= weightCurrentTotal && doorIsAlreadyOpen != false)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        Vector3 newPosition = doorOriginalTransform.position + new Vector3(0, 5, 0);
        //door.transform.position = door.transform.position + new Vector3(0, 5, 0);
        doorIsAlreadyOpen = true;
    }

    public void CloseDoor()
    {
        door.transform.position = doorOriginalTransform.position = new Vector3(0, -5, 0);
        doorIsAlreadyOpen = false;
    }
}
