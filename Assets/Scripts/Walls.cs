using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public GameObject particleSplash;

    GameObject obj;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //print("enemy Collider");
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
