using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritory : MonoBehaviour
{
    private EnemyController enemyParent;
    

    private void Start()
    {
        enemyParent = GetComponentInParent<EnemyController>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            enemyParent.target = other.transform;
                               
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            enemyParent.target = null;
            enemyParent.CancelInvoke();
            

        }
    }


}
