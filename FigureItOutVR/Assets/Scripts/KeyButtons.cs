using UnityEngine;

public class KeyButtons : MonoBehaviour
{
    public int keyNumber;

    private KeypadScript keypad;

    private void Start()
    {
        // Walk up the hierarchy to find the KeypadScript on the parent
        keypad = GetComponentInParent<KeypadScript>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            keypad.PressKey(keyNumber);
        }
    }
}
