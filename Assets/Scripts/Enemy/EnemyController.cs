using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float lookRotationSpeed = 5f;
    public GameObject particleSplash;
    public Transform target;//Gets the target when player enters the zone
    public GameObject bulletPrefab;
    public Transform firePos;
    GameObject obj;
    public bool canShoot;
   



    void Update()
    {
        
        if (target!=null)
        {
            Vector3 targetRotation = target.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation( targetRotation), lookRotationSpeed * Time.deltaTime);
            if (canShoot)
            {
                InvokeRepeating("FireBullet", 0, 0.8f);
            }
            
        }
        else if (target == null)    
        {
            canShoot = true;
           
        }

    }
    void FireBullet()
    {
        canShoot = false;
        

        PoolManager.instance.UseObject(bulletPrefab, firePos.transform.position, firePos.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            
            collision.gameObject.SetActive(false);
            PoolManager.instance.ReturnObject(collision.gameObject);
            obj = PoolManager.instance.UseObject(particleSplash, collision.transform.position, collision.transform.rotation);
            obj.SetActive(true);
            ParticleSystem particle = obj.GetComponent<ParticleSystem>();
            particle.Play();
            PoolManager.instance.ReturnObject(obj, 1f);
        }
    }
   
}
