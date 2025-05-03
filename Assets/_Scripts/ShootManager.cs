using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{

    public GameObject bullet;

    public int amountOfBullets = 50;

    private List<GameObject> bullets;
    // creating the pool of objects at the start before using it

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        bullets = new List<GameObject>();
        
        for (int i = 0; i < amountOfBullets; i++)
        {
            GameObject newBullet = Instantiate(bullet);
            ReturnToPool(newBullet);
            newBullet.GetComponent<Bullet>()._ShootManager = this;
            bullets.Add(newBullet);
        }
    }

    // get a free available object from the pool
    public GameObject GetPoolObject()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                return bullets[i];
            }
        }

        return null;
    }
    
    // return an object to the pool so we can reuse it
    public void ReturnToPool(GameObject poolObject)
    {
        Debug.Log("return object to the pool");
        poolObject.SetActive(false);
    }
    
    // destroy the objects ?


    
   
}
