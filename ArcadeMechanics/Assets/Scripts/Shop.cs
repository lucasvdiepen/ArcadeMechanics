using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    public Button bulletsButton;
    public Button pistolButton;
    public Button akButton;

    public GameObject pistol;
    public GameObject ak;

    public int bulletsCost = 10;
    public int pistolCost = 25;
    public int akCost = 50;

    public int bulletsAmount = 30;
    public int pistolBulletsAmount = 14;
    public int akBulletsAmount = 60;

    private void Start()
    {
        TextMeshProUGUI[] bulletsTexts = GetCostText(bulletsButton);
        bulletsTexts[0].text = bulletsAmount.ToString();
        bulletsTexts[1].text = bulletsCost.ToString();

        GetCostText(pistolButton)[0].text = pistolCost.ToString();

        GetCostText(akButton)[0].text = akCost.ToString();
    }

    private void OnEnable()
    {
        bulletsButton.onClick.AddListener(BuyBullets);
        pistolButton.onClick.AddListener(BuyPistol);
        akButton.onClick.AddListener(BuyAK);

        UpdateCoinsText();
    }

    private void OnDisable()
    {
        bulletsButton.onClick.RemoveAllListeners();
        pistolButton.onClick.RemoveAllListeners();
        akButton.onClick.RemoveAllListeners();
    }

    private void UpdateCoinsText()
    {
        coinsText.text = FindObjectOfType<GameManager>().coins.ToString();
    }

    private bool CanAfford(int cost)
    {
        int coins = FindObjectOfType<GameManager>().coins;
        return coins > 0 && coins >= cost;
    }

    private bool Buy(int cost)
    {
        int coins = FindObjectOfType<GameManager>().coins;
        if (coins > 0 && coins >= cost)
        {
            FindObjectOfType<GameManager>().RemoveCoins(cost);
            UpdateCoinsText();
            return true;
        }
        else return false;
    }

    private TextMeshProUGUI[] GetCostText(Button btn)
    {
        return btn.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void ExitButton()
    {
        FindObjectOfType<PlayerShop>().CloseShop();
    }

    public void BuyBullets()
    {
        if(Buy(bulletsCost))
        {
            FindObjectOfType<PlayerAttack>().AddBullets(bulletsAmount);
        }
    }

    public void BuyPistol()
    {
        if(Buy(pistolCost))
        {
            FindObjectOfType<PlayerAttack>().AddBullets(pistolBulletsAmount);
            FindObjectOfType<PlayerAttack>().SetGun(pistol);
        }
    }

    public void BuyAK()
    {
        if(Buy(akCost))
        {
            FindObjectOfType<PlayerAttack>().AddBullets(akBulletsAmount);
            FindObjectOfType<PlayerAttack>().SetGun(ak);
        }
    }
}
