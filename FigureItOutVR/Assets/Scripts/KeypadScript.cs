using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadScript : MonoBehaviour
{
    private List<int> inputAnswer = new List<int>();
    private List<int> correctAnswer = new List<int>();

    public GameObject door;
    public TextMeshPro displayText;

    private bool locked = false;

    private void Start()
    {
        GenerateRandomAnswer();
        UpdateDisplay();
    }

    public void GenerateRandomAnswer()
    {
        correctAnswer.Clear();
        for (int i = 0; i < 3; i++)
            correctAnswer.Add(Random.Range(1, 10));

        Debug.Log($"Correct Answer: {correctAnswer[0]}-{correctAnswer[1]}-{correctAnswer[2]}");
    }

    public void PressKey(int number)
    {
        if (locked || inputAnswer.Count >= 3) return;

        inputAnswer.Add(number);
        UpdateDisplay();

        if (inputAnswer.Count == 3)
            CheckAnswer();
    }

    private void CheckAnswer()
    {
        locked = true;

        if (inputAnswer[0] == correctAnswer[0] &&
            inputAnswer[1] == correctAnswer[1] &&
            inputAnswer[2] == correctAnswer[2])
        {
            displayText.text = "✓  ✓  ✓";
            displayText.color = Color.green;
            door.SetActive(false);
        }
        else
        {
            displayText.text = "✗  ✗  ✗";
            displayText.color = Color.red;
            Invoke(nameof(ResetInput), 1.2f);
        }
    }

    private void ResetInput()
    {
        inputAnswer.Clear();
        locked = false;
        displayText.color = Color.white;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        string[] slots = { "-", "-", "-" };
        for (int i = 0; i < inputAnswer.Count; i++)
            slots[i] = "*";
        displayText.text = string.Join("  ", slots);
    }

    public int GetCodeDigit(int index) => correctAnswer[index];
}