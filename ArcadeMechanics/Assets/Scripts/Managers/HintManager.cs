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

    private bool isClosing = false;

    private void Start()
    {
        animator = hintHolder.GetComponent<Animator>();
    }

    public void StartObstacleHint(ObstacleManager.ObstacleType obstacleType)
    {
        string newHintText = "";
        string newHintSubText = "";

        string moveText = "Use W A S D SPACE to move";

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

            lastHintPlayTime = Time.time;

            hintText.text = _hintText;
            hintSubText.text = _hintSubText;

            //hintText.color = new Color32((byte)(hintText.color.r * 255), (byte)(hintText.color.g * 255), (byte)(hintText.color.b * 255), 255);
            //hintSubText.color = new Color32((byte)(hintSubText.color.r * 255), (byte)(hintSubText.color.g * 255), (byte)(hintSubText.color.b * 255), 255);

            Color32 newHintTextColor = hintText.color;
            newHintTextColor.a = (byte)255;

            hintText.color = newHintTextColor;

            Color32 newHintSubTextColor = hintSubText.color;
            newHintSubTextColor.a = (byte)255;

            hintSubText.color = newHintSubTextColor;

            animator.SetTrigger("Fade_In");

            hintIsPlaying = true;
        }
    }

    private void Update()
    {
        if(hintIsPlaying)
        {
            //Check when animation is done
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("HintDone"))
            {
                if (hintHolder.activeSelf && isClosing)
                {
                    isClosing = false;
                    hintHolder.SetActive(false);

                    hintIsPlaying = false;
                    Debug.Log("HintDone");
                }
            }

            //Check if hint text should stop showing
            float time = Time.time;
            if (time >= (lastHintPlayTime + hintPlayTime) && !isClosing && hintIsPlaying)
            {
                Debug.Log("Hint is closing now");
                isClosing = true;

                animator.SetTrigger("Fade_Out");
            }
        }
    }
}
