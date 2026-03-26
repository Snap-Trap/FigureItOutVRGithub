using UnityEngine;
using TMPro;

public class CodeDisplayNumbers : MonoBehaviour
{
    public TextMeshProUGUI displayText;

    public void SetDigit(int digit)
    {
        displayText.text = digit.ToString();
    }
}
