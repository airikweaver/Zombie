using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnim;

    private void Start()
    {
        doorAnim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            doorAnim.SetBool("open", true);
        }
    }
    private void Update()
    {
    }
}
