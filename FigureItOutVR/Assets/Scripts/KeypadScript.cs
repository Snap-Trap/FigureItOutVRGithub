using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadScript : MonoBehaviour
{
    private List<string> inputAnswer = new List<string>();
    private string correctAnswer;

    public GameObject door;

    public void GenerateRandomAnswer()
    {
        // for (int )
        correctAnswer = Random.Range(1, 10).ToString();

        Debug.Log("Correct Answer: " + correctAnswer);
    }
}
