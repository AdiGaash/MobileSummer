using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ShootManager _ShootManager;
    private void OnEnable()
    {
        Invoke("DestroyBulletAfterTime", 2f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            return;
        }
        DestroyBullet();
    }

    void DestroyBulletAfterTime()
    {
        DestroyBullet();
    }

    void DestroyBullet()
    {
        if (_ShootManager != null)
        {
            _ShootManager.ReturnToPool(this.gameObject);
        }
        else
        {
            Debug.Log("destroy bullet gameobject");
            Destroy(gameObject);
        }
        
    }



}
