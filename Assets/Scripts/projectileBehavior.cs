using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileBehavior : MonoBehaviour
{
    public float speed;
    public GameObject hitFX;
    public MeshRenderer rend;
    public bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground" || other.tag == "Default")
        {
            GameObject fx = Instantiate(hitFX, transform.position, transform.rotation);
            Destroy(fx, 2f);
            rend.enabled = false;
            hit = true;
            Destroy(gameObject, 3f);
        }
    }
}
