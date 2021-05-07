using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Settings")]

    [Range(0, 10)] public int damage;
    public float fireRate = 0.5f;
    public float reloadTime = 3f;
    public float range = 100f;
    public int maxAmmo = 10;
    public float currentAmmo;

    [Header("Booleans")]
    public bool isReloading = false;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

    }
    public IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        FindObjectOfType<AudioManager>().Play("Pistol Reload");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

}
