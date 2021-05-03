
using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float fireRate = 0.5f;
    [SerializeField]
    private Transform firePoint;
    [Header("Settings")]
    public int damage = 5;
    public float range = 100f;
    private float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                timer = 0f;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward * 100, Color.red, 2f);
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, range))
        {
            var health = hitInfo.collider.GetComponent<EnemyAI>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }


        }


    }
}
