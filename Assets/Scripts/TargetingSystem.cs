using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public GameObject target;
    public LayerMask targetable;
    public Camera cam;
    public Vector3 mousePos;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition + offset);

        target.transform.position = new Vector3(mousePos.x, mousePos.y, target.transform.position.z);
    }
}
