using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextInput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputField;
    public delegate void TextReceived(string text);
    public event TextReceived OnTextReceived;
    public void ConfirmPressed()
    {
        string text = inputField.text;
        if (true)//text.All(x => char.IsLetterOrDigit(x) || char.IsWhiteSpace(x)))
        {
            OnTextReceived?.Invoke(text);
        }
        else
        {
            //Debug.LogWarning("Input Text must be Alphanumeric! Event will not be triggered! (Entered Text: " + text + ")");
        }
    }
}
