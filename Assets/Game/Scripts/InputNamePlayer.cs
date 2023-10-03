using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputNamePlayer : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;
    public void EnterName()
    {
        string inputValue = inputField.text;
        if (string.IsNullOrEmpty(inputValue) || string.IsNullOrWhiteSpace(inputValue))
            return;
        GameManager.Instance.PlayerName = inputField.text;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mainmenu");
    }
}
