using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Info")]
    private GameObject player;
    NavMeshAgent agent;
    PlayerController playerController;
    Transform target;
    Animator anim;
    public Transform moveSpot;
    Vector3 startingVector;
    public HealthBar healthBar;

    [Header("Settings")]
    public float lookRadius = 10f;
    public float speed = 3.5f;
    public int startingHealth = 20;

    [Header("Visualization")]
    public int currentHealth;
    private float waitTime;
    public float startWaitTime;
    public bool isDead = false;


    void Start()
    {
        currentHealth = startingHealth;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        target = player.transform;
        startingVector = transform.position;
        waitTime = startWaitTime;
        moveSpot.position = new Vector3(Random.Range(startingVector.x + 10, startingVector.x - 10), 0, Random.Range(startingVector.z + 10, startingVector.z - 10));

    }
    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
            isDead = true;
        }
    }
    void Update()
    {
        if (!isDead)
        {
            if (calculateDistance() > lookRadius)
            {
                Patrol();
            }
            agent.speed = speed;
            Animations();
            if (calculateDistance() <= lookRadius && !playerController.isDead())
            {
                agent.SetDestination(target.position);
            }
        }
        else
        {
            speed = 0;
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void Animations()
    {
        if (calculateDistance() <= lookRadius && calculateDistance() > agent.stoppingDistance && !playerController.isDead())
        {
            speed = 3.5f;
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
        }
        else if (Vector3.Distance(transform.position, moveSpot.position) > 0.2f)
        {
            speed = 0.5f;
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);
        }
        else if (calculateDistance() > lookRadius || calculateDistance() <= agent.stoppingDistance || Vector3.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            speed = 0;
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
        }
        if (calculateDistance() <= agent.stoppingDistance && !playerController.isDead())
        {
            anim.SetBool("isAttacking", true);
            anim.SetBool("isIdle", false);
            FaceTarget();
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }
    private void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
        FaceTargetPoint();
        if (Vector3.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector3(Random.Range(startingVector.x + 10, startingVector.x - 10), 0, Random.Range(startingVector.z + 10, startingVector.z - 10));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    void FaceTargetPoint()
    {
        Vector3 direction = (moveSpot.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private float calculateDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return distance;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
