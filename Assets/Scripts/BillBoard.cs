using UnityEngine;

public class BillBoard : MonoBehaviour
{
    GameObject cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
