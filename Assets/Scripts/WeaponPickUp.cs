using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponPickUp : FindClosestItem
{
    public Transform equipPosition;


    GameObject weapon;
    GameObject currentWeapon;
    PlayerController playerController;
    bool canGrab = true;
    public bool inRange = false;
    GameObject clone;


    

    [Header("References")]
    public GameObject PressEPrefab;
    [SerializeField] GameObject[] objs;
    public Rig armRig1;
    public Rig armRig2;
    [SerializeField] GameObject closestItem = null;

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
        FindClosestItem();
        if (playerController.isDead())
        {
            Drop();
        }
        if (IsInRange())
        {
            if (Input.GetKeyDown(KeyCode.E) && canGrab)
            {

                if (currentWeapon != null)
                {
                    Drop();
                }
                Pickup();

            }
        }

        if (currentWeapon != null)
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
        if (dist < RangeToGrab)
        {
            return inRange = true;
        }
        else
        {
            return inRange = false;
        }
    }
    void FindClosestItem()
    {
        float distanceToItem;
        float distanceToClosestItem = Mathf.Infinity;
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("canGrab");

        foreach (GameObject currentItem in allItems)
        {
            clone = Instantiate(PressEPrefab, currentItem.transform.position, Quaternion.identity);
            Destroy(clone);
            distanceToItem = (currentItem.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToItem < distanceToClosestItem)
            {
                distanceToClosestItem = distanceToItem;
                closestItem = currentItem;
                weapon = currentItem;
            }
        }
        Debug.DrawLine(this.transform.position, closestItem.transform.position);
    }


    public void Pickup()
    {
        armRig1.weight = 1;
        armRig2.weight = 1;
        currentWeapon = weapon;
        currentWeapon.transform.position = equipPosition.position;
        currentWeapon.transform.parent = equipPosition;
        currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
        canGrab = false;
    }
    public void Drop()
    {
        armRig1.weight = 0;
        armRig2.weight = 0;
        if (!playerController.isDead())
        {
            currentWeapon.transform.parent = null;
            currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
            currentWeapon = null;
            canGrab = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
