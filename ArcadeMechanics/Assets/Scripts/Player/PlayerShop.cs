using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShop : MonoBehaviour
{
    public Canvas shopCanvas;

    private bool shopIsInRange = false;

    [HideInInspector] public bool shopIsOpened = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(shopIsInRange && !shopIsOpened && !FindObjectOfType<GameManager>().isPaused)
            {
                //Open shop dialog here
                OpenShop();
            }
        }
    }

    public void OpenShop()
    {
        shopIsOpened = true;
        shopCanvas.gameObject.SetActive(true);
        FindObjectOfType<PlayerMovement>().freezeMovement = true;
    }

    public void CloseShop()
    {
        shopIsOpened = false;
        FindObjectOfType<PlayerMovement>().freezeMovement = false;
        shopCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Shop")
        {
            //Shop is in range
            shopIsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Shop")
        {
            //Shop is out of range
            shopIsInRange = false;
        }
    }
}
