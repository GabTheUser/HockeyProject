using UnityEngine;
using TMPro;

public class KeyBoardUI : MonoBehaviour
{
    public TMP_InputField JoinInput;
    
    public void PressKey()
    {
        JoinInput.text += GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public void RemoveLast()
    {
        JoinInput.text = JoinInput.text.Remove(JoinInput.text.Length - 1);
    }
}
