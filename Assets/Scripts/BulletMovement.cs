using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float moveSpeed;
    //public GameObject partcleSplash;
 
    
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime , Space.Self );
    }
   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            print("Bullet class");
            PoolManager.instance.ReturnObject(gameObject);


        }
    }
    IEnumerator ReturnParticleToPool(GameObject returnObj)
    {
        yield return new WaitForSeconds(1f);
        PoolManager.instance.ReturnObject(returnObj);
    }*/
}
