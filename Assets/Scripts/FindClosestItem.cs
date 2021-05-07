using UnityEngine;

public class FindClosestItem : MonoBehaviour
{
    public GameObject closestItem = null;
    WeaponPickUp weaponPickUp;

    // Start is called before the first frame update
    void Start()
    {
        weaponPickUp = GetComponent<WeaponPickUp>();
    }
    // Update is called once per frame
    void Update()
    {
        ClosestItem();
    }
    void ClosestItem()
    {
        float distanceToItem;
        float distanceToClosestItem = Mathf.Infinity;
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("canGrab");

        foreach (GameObject currentItem in allItems)
        {

            distanceToItem = (currentItem.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToItem < distanceToClosestItem)
            {
                distanceToClosestItem = distanceToItem;
                closestItem = currentItem;
                weaponPickUp.weapon = currentItem;
            }
        }
        if (closestItem != null)
        {
            Debug.DrawLine(this.transform.position, closestItem.transform.position);
        }
        
    }
}
