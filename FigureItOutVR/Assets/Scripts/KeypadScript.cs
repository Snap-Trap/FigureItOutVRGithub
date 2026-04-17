using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Netcode;

public class KeypadScript : NetworkBehaviour
{
    private List<int> inputAnswer = new List<int>();

    private NetworkVariable<int> correctAnswer0 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private NetworkVariable<int> correctAnswer1 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private NetworkVariable<int> correctAnswer2 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public DoorController door;
    public TextMeshProUGUI displayText;

    private bool doorLocked = false;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            correctAnswer0.Value = Random.Range(1, 10);
            correctAnswer1.Value = Random.Range(1, 10);
            correctAnswer2.Value = Random.Range(1, 10);
            Debug.Log($"The random spaghetti of the day is: {correctAnswer0.Value}-{correctAnswer1.Value}-{correctAnswer2.Value}");
        }

        UpdateDisplay();
    }

    public void PressKey(int number)
    {
        if (doorLocked || inputAnswer.Count >= 3) return;

        inputAnswer.Add(number);
        UpdateDisplay();

        if (inputAnswer.Count == 3)
        {
            CheckAnswer();
        }
    }

    private void CheckAnswer()
    {
        doorLocked = true;

        if (inputAnswer[0] == correctAnswer0.Value &&
            inputAnswer[1] == correctAnswer1.Value &&
            inputAnswer[2] == correctAnswer2.Value)
        {
            displayText.text = "Correct";
            displayText.color = Color.green;
            door.OpenDoor();
        }
        else
        {
            displayText.text = "Wrong";
            displayText.color = Color.red;
            StartCoroutine(ResetInput());
        }
    }

    private IEnumerator ResetInput()
    {
        yield return new WaitForSeconds(1f);
        inputAnswer.Clear();
        doorLocked = false;
        displayText.color = Color.white;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        string[] slots = { "-", "-", "-" };
        for (int i = 0; i < inputAnswer.Count; i++)
        {
            slots[i] = "*";
        }
        displayText.text = string.Join("  ", slots);
    }

    public int GetCodeDigit(int index)
    {
        if (index == 0) return correctAnswer0.Value;
        if (index == 1) return correctAnswer1.Value;
        return correctAnswer2.Value;
    }
}