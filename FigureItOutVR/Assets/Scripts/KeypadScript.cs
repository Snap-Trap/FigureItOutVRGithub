using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeypadScript : MonoBehaviour
{
    private List<int> inputAnswer = new List<int>();
    private List<int> correctAnswer = new List<int>();

    public GameObject door;
    public TextMeshProUGUI displayText;

    private bool doorLocked = false;

    private void Start()
    {
        GenerateRandomAnswer();
        UpdateDisplay();
    }

    public void GenerateRandomAnswer()
    {
        correctAnswer.Clear();
        for (int i = 0; i < 3; i++)
        {
            correctAnswer.Add(Random.Range(1, 10));
        }

        Debug.Log($"The random spaghetti of the day is: {correctAnswer[0]}-{correctAnswer[1]}-{correctAnswer[2]}");
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

        if (inputAnswer[0] == correctAnswer[0] && inputAnswer[1] == correctAnswer[1] && inputAnswer[2] == correctAnswer[2])
        {
            displayText.text = "Correct";
            displayText.color = Color.green;
            door.SetActive(false);
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
        doorLocked = true;
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

    public int GetCodeDigit(int index) => correctAnswer[index];
}