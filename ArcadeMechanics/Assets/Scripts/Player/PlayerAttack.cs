using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject gunHolder;

    public TextMeshProUGUI ammoText;

    [HideInInspector] public bool shootingAllowed = true;

    [HideInInspector] public int totalBullets = 0;

    private GameObject gun;
    private Gun gunScript;
    private PlayerMovement playerMovement;

    [HideInInspector] public int bullets = 0;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (gunScript != null)
        {
            if(Input.GetKey(KeyCode.F))
            {
                if(shootingAllowed) gunScript.Shoot(playerMovement.lastMoveDirection);
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                gunScript.Reload();
            }
        }
    }

    public void UpdateAmmoText()
    {
        ammoText.text = totalBullets + " | " + bullets;
    }

    public void SetGun(GameObject newGun)
    {
        DestroyGun();

        gun = Instantiate(newGun);
        gun.transform.parent = gunHolder.transform;
        gun.transform.localPosition = Vector3.zero;
        gunScript = gun.GetComponent<Gun>();
    }

    public void AddBullets(int amount)
    {
        totalBullets += amount;

        UpdateAmmoText();
    }

    public void RemoveBullets(int amount)
    {
        totalBullets -= amount;
        if (totalBullets < 0) totalBullets = 0;

        UpdateAmmoText();
    }

    public int GetReloadAmount(int maxBullets)
    {
        if (totalBullets >= maxBullets) return maxBullets;
        else return totalBullets;
    }

    public void SetLoadedBullets(int _bullets)
    {
        bullets = _bullets;

        UpdateAmmoText();
    }

    private void DestroyGun()
    {
        if (gun != null)
        {
            Destroy(gun);
            gun = null;
            gunScript = null;
        }
    }

    public void ResetPlayerAttack()
    {
        DestroyGun();
        totalBullets = 0;
        bullets = 0;
        UpdateAmmoText();
        shootingAllowed = true;
    }
}
