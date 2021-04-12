using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardNameInput : MonoBehaviour
{
    public InputField nameInput;

    public Button submitButton;

    private void SubmitButtonClicked()
    {

    }

    private void OnEnable()
    {
        submitButton.onClick.AddListener(SubmitButtonClicked);
    }

    private void OnDisable()
    {
        submitButton.onClick.RemoveAllListeners();
    }
}
