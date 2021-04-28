using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public Transform equipPosition;
    public float distance = 5f;
    public GameObject PressEPrefab;
    public GameObject[] objs;
    bool canGrab = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canGrab)
        {
            Pickup();
        }
        if (Input.GetKeyDown(KeyCode.Q) && !canGrab)
        {
            Drop();
        }
        Inspect();
    }
    public void Inspect()
    {
        objs = GameObject.FindGameObjectsWithTag("canGrab");
        foreach (GameObject obj in objs)
        {
            if (obj.transform.position != equipPosition.position)
            {
                Instantiate(PressEPrefab, obj.transform.position, obj.transform.rotation);
            }
            Destroy(obj);
        }
            
    }
    public void Pickup()
    {
        
        objs = GameObject.FindGameObjectsWithTag("canGrab");
        foreach (GameObject obj in objs)
        {

            if (transform.position.x < distance || transform.position.y < distance || transform.position.z < distance)
            {
                obj.transform.position = equipPosition.position;
            }
            canGrab = false;
        }
        

    }
    public void Drop()
    {
        canGrab = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
