using UnityEngine;

public class KeyNumbers : MonoBehaviour
{
    // DO NOT FUCKING CHANGE THIS NUMBER I DID NOT ACCOUNT FOR THAT IN THE OTHER SCRIPT I'LL KILL YOU
    public CodeDisplayNumbers[] numberObjects = new CodeDisplayNumbers[3];
    public Transform[] spawnLocations = new Transform[7];

    private KeypadScript keypad;

    private void Start()
    {
        keypad = FindObjectOfType<KeypadScript>();

        if (keypad == null) return;
        SpawnNumbers();
    }

    private void SpawnNumbers()
    {
        Transform[] shuffled = ShuffleTransforms(spawnLocations);

        for (int i = 0; i < 3; i++)
        {
            int digit = keypad.GetCodeDigit(i);
            
            numberObjects[i].transform.position = shuffled[i].position;
            numberObjects[i].transform.rotation = shuffled[i].rotation;
            numberObjects[i].SetDigit(digit);
            numberObjects[i].gameObject.SetActive(true);
        }
    }

    private Transform[] ShuffleTransforms(Transform[] original)
    {
        Transform[] copy = (Transform[])original.Clone();
        for (int i = copy.Length - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            (copy[i], copy[rand]) = (copy[rand], copy[i]);
        }
        return copy;
    }
}
