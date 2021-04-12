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
        int currentScore = FindObjectOfType<GameManager>().score;

        string name = nameInput.text;

        FindObjectOfType<Leaderboard>().UpdateLeaderboard(currentScore, name);
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
