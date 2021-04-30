using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;


    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float maxSpeed = 5.5f;
    public int maxHealth = 100;
    public HealthBar healthBar;
    [Header("Info")]
    [SerializeField] float Gravity = 9.85f;
    [SerializeField] bool playerDead = false;
     public int currentHealth;
     public float speed = 4f;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }
    // Update is called once per frame
    void Update()
    {
        isDead();
        if (currentHealth > 0)
        {
            Move();
            DoGravity();
        }

    }
    public void DoGravity()
    {
        controller.Move(Vector3.down * Gravity * Time.deltaTime);
    }
    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }
    }
    public bool isDead()
    {
        if (currentHealth > 0)
        {
            playerDead = false;
        }
        if (currentHealth <= 0)
        {
            playerDead = true;
        }
        return playerDead;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

    }
}