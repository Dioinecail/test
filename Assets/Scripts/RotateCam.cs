using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{
    public Transform target;
    public float dampH;
    public float dampV;
    public CharacterControl1 player;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        float Xpos = Mathf.Lerp(transform.position.x, target.position.x, dampH);
        float Ypos = Mathf.Lerp(transform.position.y, target.position.y, dampV);

        Vector3 newPos = new Vector3(Xpos, Ypos, target.position.z);

        transform.position = Vector3.Lerp(transform.position, newPos, dampH * Time.deltaTime);  
    }
       
}
