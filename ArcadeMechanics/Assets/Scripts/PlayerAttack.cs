using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject gunHolder;

    public TextMeshProUGUI ammoText;

    private GameObject gun;
    private Gun gunScript;

    public GameObject testGun;

    private void Start()
    {
        SetGun(testGun);
    }

    private void Update()
    {
        if (gunScript != null)
        {
            if(Input.GetKey(KeyCode.F))
            {
                gunScript.Shoot(transform.rotation.x);
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                gunScript.Reload();
            }
        }
    }

    public void UpdateAmmoText(int bullets)
    {
        ammoText.text = "Ammo: " + bullets;
    }

    public void SetGun(GameObject newGun)
    {
        DestroyGun();

        gun = Instantiate(newGun);
        gun.transform.parent = gunHolder.transform;
        gun.transform.localPosition = Vector3.zero;
        gunScript = gun.GetComponent<Gun>();
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
        UpdateAmmoText(0);
    }
}
