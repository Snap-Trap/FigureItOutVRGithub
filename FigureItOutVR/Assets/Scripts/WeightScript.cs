using UnityEngine;

public class WeightScript : MonoBehaviour
{
    public PressurePlateScript pressurePlate;

    public int weightValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PressurePlate"))
        {
            pressurePlate.weightCurrentTotal += weightValue;
        }
    }
}
