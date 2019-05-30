using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotation : MonoBehaviour
{
    public GameObject tower;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.Rotate(0f, -90, 0f);
            tower.transform.Rotate(0f, -90, 0f);

            Destroy(gameObject);
        }
    }
}
