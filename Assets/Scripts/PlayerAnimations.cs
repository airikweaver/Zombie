using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    bool canSprint = true;
    Animator anim;
    PlayerController player;
    private float health = 0;
    public float sprintCooldown = 3;
    public float sprintDuration = 3;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        health = player.currentHealth;
        Animations();
    }
    void CanDie()
    {
        anim.SetFloat("Health", health);
    }
    public void IsSprinting()
    {
        if (canSprint && Input.GetKey(KeyCode.LeftShift) && !player.isDead())
        {
            canSprint = false;
            player.speed += 1.5f;
            anim.SetBool("isSprinting", true);
        }
        if (!canSprint && Input.GetKeyUp(KeyCode.LeftShift) && !player.isDead())
        {
            canSprint = true;
            player.speed -= 1.5f;
            anim.SetBool("isSprinting", false);
        }
    }
  
    public void AnimationSpeed()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * player.speed;
        float vertical = Input.GetAxisRaw("Vertical") * player.speed;

        horizontal = Mathf.Abs(horizontal);
        vertical = Mathf.Abs(vertical);

        if (horizontal > vertical)
        {
            anim.SetFloat("Speed", horizontal);
        }
        if (vertical > horizontal)
        {
            anim.SetFloat("Speed", vertical);
        }
        if (vertical == horizontal)
        {
            anim.SetFloat("Speed", vertical);
        }

    }
    public void Animations()
    {
        CanDie();
        IsSprinting();
        AnimationSpeed();
    }
   
}

