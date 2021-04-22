using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    bool sprint = false;
    private float speed = 4f;
    Animator anim;
    private float health = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        health = GameObject.Find("Player").GetComponent<PlayerController>().currentHealth;
        Animations();
    }
    void CanDie()
    {
        anim.SetFloat("Health", health);
    }
    public void IsSprinting()
    {
        if (!sprint && Input.GetKey(KeyCode.LeftShift))
        {
            sprint = true;
            speed += 1f;
        }
        else if (sprint && Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprint = false;
            speed -= 1f;
        }
    }
    public void AnimationSpeed()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * speed;
        float vertical = Input.GetAxisRaw("Vertical") * speed;

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

