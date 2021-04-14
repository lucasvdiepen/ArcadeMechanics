using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public GameObject hintObject;

    private TextMeshProUGUI hintText;
    private TextMeshProUGUI hintSubText;
    private Animator animator;

    private bool isHintPlaying = false;

    private float lastHintPlayTime = 0;

    public float hintPlayTime = 4f;

    private void Start()
    {
        TextMeshProUGUI[] texts = hintObject.GetComponents<TextMeshProUGUI>();
        hintText = texts[0];
        hintSubText = texts[1];

        animator = hintObject.GetComponent<Animator>();
    }

    public void StartObstacleHint(ObstacleManager.ObstacleType obstacleType)
    {

    }

    private void PlayHint(string text)
    {
        if(!isHintPlaying)
        {
            isHintPlaying = true;

            //Enable hint here
        }
    }

    private void Update()
    {
        float time = Time.time;
        if(time >= (lastHintPlayTime + hintPlayTime))
        {
            //Disable hint here
        }
    }
}
