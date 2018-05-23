using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLook : MonoBehaviour {


    Transform m_CamT;

    // Use this for initialization
    void Start ()
    {
        m_CamT = Camera.main.transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        look();
	}



    void look()
    {
      

        transform.LookAt(m_CamT);
        
    }

    public void CanOff()
    {
        gameObject.SetActive(false);
    }

    public void CanOn()
    {
        gameObject.SetActive(true);
    }
}
