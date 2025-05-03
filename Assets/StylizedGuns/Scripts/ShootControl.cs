using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    
    public Transform shootPoint;
    public GameObject bullet;
    public Animator gunAnimator;
    public bool UsePool = false;
    public ShootManager _ShootManager;

    private void Update()
    {
        if (Input.GetButton("Fire1")){
            //gunAnimator.SetBool("IsShooting", true); 
            if(!UsePool)
                Shoot();
            else
            {
                ShootWithPool();
            }
        }

    }

    private void ShootWithPool()
    {
        GameObject myBullet = _ShootManager.GetPoolObject();
        if (myBullet != null)
        {
            myBullet.transform.position = shootPoint.position;
            myBullet.transform.rotation = shootPoint.rotation;
            myBullet.SetActive(true);
            myBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 2000);
        }
    }


    public void Shoot()
    {
            //StopShootingAn();

            GameObject newBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 2000);
            //StopShootingAn();


    }

    public void StopShootingAn()
    {

            gunAnimator.SetBool("IsShooting", false);

        
    }
}
