using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPos : MonoBehaviour
{
    public Transform playerPos;
    public Vector3 initialPlayerPos;

    public Animator fadePannel;
    public GameObject menuPannel;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPlayerPos = playerPos.position;
    }




    private void OnTriggerEnter(Collider other)
    {
        fadePannel.SetBool("FadeOut",true);
        fadePannel.SetBool("FadeIn",false);
        Invoke("MenuActivate", 0.8f);
    }
    public void Button_Start()
    {
        Invoke("StartPannelAnim", 0.5f);
        menuPannel.SetActive(false);
        playerPos.position = initialPlayerPos;
    }
    public void StartPannelAnim()
    {
        fadePannel.SetBool("FadeIn", true);
        fadePannel.SetBool("FadeOut", false);
        
    }
    public void MenuActivate()
    {
        menuPannel.SetActive(true);
    }
}
