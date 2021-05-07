using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ReloadBarUI : MonoBehaviour
{
    private WeaponPickUp weaponPickUp;
    private Gun gun;
    public Transform LoadingBar;
    public Transform TextIndicator;
    public Transform TextReloading;
    [SerializeField] private float fillSpeed;
    [SerializeField] private float currentAmount;
    // Start is called before the first frame update
    void Start()
    {
        weaponPickUp = GameObject.Find("Player").GetComponent<WeaponPickUp>();
        gun = GameObject.Find("Player").GetComponent<Gun>();
        
    }

    // Update is called once per frame
    void Update()
    {
        currentAmount = gun.WeaponAmmoBeingUsed();
        
        if (weaponPickUp.currentWeapon != null)
        {
            TextIndicator.GetComponent<Text>().text = currentAmount.ToString();
            if (currentAmount <= 0)
            {
                TextReloading.gameObject.SetActive(true);
            }
            else
            {
                TextReloading.gameObject.SetActive(false);
            }
            LoadingBar.GetComponent<Image>().fillAmount = currentAmount / 10;
        }
        
    }
}
