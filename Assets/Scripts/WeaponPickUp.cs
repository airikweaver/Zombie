using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponPickUp : MonoBehaviour
{




    [Header("Info")]
    public GameObject weapon;
    public GameObject currentWeapon;
    PlayerController playerController;
    public bool canGrab = true;
    public bool inRange = false;
    public Transform equipPosition;

    [Header("References")]
    public GameObject PressEPrefab;
    public GameObject[] objs;
    public Rig armRig1;
    public Rig armRig2;


    [Header("Settings")]
    public float RangeToGrab = 2f;
    public float distance = 5f;
    [SerializeField] float dist;


    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        //if (playerController.isDead())
        //{
        //Drop();
        //}
        if (weapon != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && canGrab)
            {
                Pickup();
            }
        }
        if (weapon != null)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !canGrab)
            {
                Drop();
            }
        }
        
    }
    public bool IsInRange()
    {

        dist = Vector3.Distance(transform.position, weapon.transform.position);
        if (dist < RangeToGrab && canGrab)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
        return inRange;

    }
    public void Pickup()
    {
        playSoundOnPickup();
        armRig1.weight = 1;
        armRig2.weight = 1;
        currentWeapon = weapon;
        currentWeapon.transform.position = equipPosition.position;
        currentWeapon.transform.parent = equipPosition;
        currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
        canGrab = false;
    }
    public void playSoundOnPickup()
    {
        if (weapon.name == "M1911")
        {
            FindObjectOfType<AudioManager>().Play("Weapon Cock");
        }
    }
    public void Drop()
    {
        armRig1.weight = 0;
        armRig2.weight = 0;
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon = null;
        canGrab = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
