using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardNameInput : MonoBehaviour
{
    public TMP_InputField nameInput;

    public Button submitButton;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SubmitButtonClicked();
        }
    }

    private void SubmitButtonClicked()
    {
        int currentScore = FindObjectOfType<GameManager>().score;

        string name = nameInput.text;
        //if (string.IsNullOrEmpty(name)) name = "Unknown";

        FindObjectOfType<Leaderboard>().UpdateLeaderboard(currentScore, name);

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        nameInput.text = "";

        submitButton.onClick.AddListener(SubmitButtonClicked);
    }

    private void OnDisable()
    {
        submitButton.onClick.RemoveAllListeners();
    }
}
