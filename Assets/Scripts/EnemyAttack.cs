using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float enemyCooldown = 0.5f;
    public int damage = 5;

    private bool playerInRange = false;
    private bool canAttack = true;

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && canAttack)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().TakeDamage(damage);
            StartCoroutine(AttackCooldown());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
    }
}
