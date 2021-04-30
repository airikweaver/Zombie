using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float enemyCooldown = 0.5f;
    public int damage = 5;

    PlayerController player;
    private bool playerInRange = false;
    private bool canAttack = true;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (playerInRange && canAttack && !player.isDead())
        {
            FindObjectOfType<AudioManager>().Play("ZombieAttack");
            player.TakeDamage(damage);
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
