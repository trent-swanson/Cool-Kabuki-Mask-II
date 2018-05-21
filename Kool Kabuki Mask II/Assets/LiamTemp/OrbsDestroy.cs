using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsDestroy : MonoBehaviour {

    private GameObject m_Player;

    Orbs BoolOrb;

    private float m_prox = 2;

   

    // Use this for initialization
    void Start ()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        destrction();
	}



   void destrction()
    {
        if (Input.GetAxisRaw("Use") > 0)
        {
            if (Vector3.Distance(m_Player.transform.position, transform.position) <= m_prox)
            {

                BoolOrb.SetDead(true);
                Destroy(this);
            }
        }
    }



}
