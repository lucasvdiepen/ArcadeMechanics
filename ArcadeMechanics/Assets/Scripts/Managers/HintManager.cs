using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public TextMeshProUGUI hintText;
    public TextMeshProUGUI hintSubText;
    public GameObject hintHolder;
    private Animator animator;

    private bool hintIsPlaying = false;

    private float lastHintPlayTime = 0;

    public float hintPlayTime = 4f;

    //private bool isClosing = false;

    private void Start()
    {
        animator = hintHolder.GetComponent<Animator>();
    }

    public void StartObstacleHint(ObstacleManager.ObstacleType obstacleType)
    {
        string newHintText = "";
        string newHintSubText = "";

        string moveText = "Use W A S D and SPACE to move";

        //Determines what hint text should show
        if(obstacleType == ObstacleManager.ObstacleType.Boss)
        {
            newHintText = "Defeat the boss";
            newHintSubText = moveText + " and jump on enemy to deal damage";

            if (FindObjectOfType<PlayerAttack>().hasGun)
            {
                newHintSubText += "\nUse F to shoot and R to reload";
            }
        }
        else if(obstacleType == ObstacleManager.ObstacleType.ObstacleBoss)
        {
            newHintText = "Defeat the boss";
            newHintSubText = moveText + " and try to get past the boss";

            if(FindObjectOfType<PlayerAttack>().hasGun)
            {
                newHintSubText += "\nUse F to shoot and R to reload";
            }
        }
        else if(obstacleType == ObstacleManager.ObstacleType.Shop)
        {
            Debug.Log("in shop if");
            newHintText = "Shop";
            newHintSubText = moveText + " and use F to interact with the shop";
        }

        PlayHint(newHintText, newHintSubText);
    }

    private void PlayHint(string _hintText, string _hintSubText)
    {
        if(!hintIsPlaying)
        {
            Debug.Log("Hint start");

            hintHolder.SetActive(true);

            hintText.text = _hintText;
            hintSubText.text = _hintSubText;

            animator.SetTrigger("FadeEffect");

            hintIsPlaying = true;
        }
    }

    private void LateUpdate()
    {
        if(hintIsPlaying)
        {
            //Check when animation is done
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("HintDone"))
            {
                hintIsPlaying = false;
                hintHolder.SetActive(false);
                Debug.Log("HintDone");
            }
        }
    }
}
