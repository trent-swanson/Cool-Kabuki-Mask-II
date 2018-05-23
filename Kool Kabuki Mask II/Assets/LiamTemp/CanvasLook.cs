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
        Vector3 Direction = m_CamT.transform.position - transform.position;
       

        Direction.Normalize();

        transform.LookAt(Direction);
        
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
