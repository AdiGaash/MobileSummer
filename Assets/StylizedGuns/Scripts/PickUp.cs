using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject pickUpGun;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&pickUpGun!=null)
        {
            GameObject[] allGuns = GameObject.FindGameObjectsWithTag("Gun");
            for(int i=0; i<allGuns.Length; i++)
            {
                allGuns[i].SetActive(false);
            }
            pickUpGun.SetActive(true);
        }
    }

}
