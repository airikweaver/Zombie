using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    Pistol pistol;
    WeaponPickUp weaponPickUp;
    public GameObject muzzleFlash;
    public GameObject bulletCollision;
    [SerializeField] private Transform firePoint;
    [Header("Settings")]
    private float timer;
    public bool isShooting = false;
    public bool pickedUpGun = false;
    private void Start()
    {
        pistol = GameObject.Find("M1911").GetComponent<Pistol>();
        weaponPickUp = GetComponent<WeaponPickUp>();
    }
    // Update is called once per frame
    void Update()
    {
        Timer();
        CheckIfEquipped();
    }

    private void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= WeaponFireRate())
        {
            if (Input.GetButtonDown("Fire1") && CheckIfEquipped())
            {
                timer = 0f;
                Shoot();
            }
        }
    }

    public bool CheckIfEquipped()
    {
        if (weaponPickUp.currentWeapon != null)
        {
            pickedUpGun = true;
        }
        else
        {
            pickedUpGun = false;
        }
        return pickedUpGun;
    }
    private void Shoot()
    {
        isShooting = true;

        if (CheckIfEquipped() && WeaponAmmoBeingUsed() > 0)
        {
            FindObjectOfType<AudioManager>().Play("M1911 Gunshot");
            SubtractAmmoFromWeapon();
            Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward * 100, Color.red, 2f);
            Ray ray = new Ray(firePoint.position, firePoint.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, WeaponRange()))
            {
                var health = hitInfo.collider.GetComponent<EnemyAI>();
                Instantiate(bulletCollision, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Instantiate(muzzleFlash, weaponPickUp.currentWeapon.transform.position, Quaternion.identity, transform);
                if (health != null)
                {
                    health.TakeDamage(WeaponDamage());
                }
            }
        }
        isShooting = false;
    }
    private float WeaponFireRate()
    {
        if (weaponPickUp.weapon.name == "M1911")
        {
            return pistol.fireRate;
        }
        else
        {
            return 1;
        }
    }
    private int WeaponDamage()
    {
        if (weaponPickUp.weapon.name == "M1911")
        {
            return pistol.damage;
        }
        else
        {
            return 1;
        }
    }
    public void SubtractAmmoFromWeapon()
    {
        if (weaponPickUp.weapon.name == "M1911")
        {
            pistol.currentAmmo--;
        }
    }
    public float WeaponAmmoBeingUsed()
    {
        if (weaponPickUp.weapon.name == "M1911")
        {
            return pistol.currentAmmo;
        }
        else
        {
            return 1;
        }
    }
    public float WeaponReloadSpeed()
    {
        if (weaponPickUp.weapon.name == "M1911")
        {
            return pistol.reloadTime;
        }
        else
        {
            return 1;
        }
    }
    private float WeaponRange()
    {
        if (weaponPickUp.weapon.name == "M1911")
        {
            return pistol.range;
        }
        else
        {
            return 1;
        }
    }
    
}
