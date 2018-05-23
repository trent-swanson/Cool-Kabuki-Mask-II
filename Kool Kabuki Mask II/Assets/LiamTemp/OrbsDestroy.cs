using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsDestroy : MonoBehaviour {

    private GameObject m_Player;

    [SerializeField]
    CanvasLook Loook;

    [SerializeField]
    GameController Controll;

    [SerializeField]
    private float m_prox = 2;

    [SerializeField]
    GameObject m_ClenseParticle = null;

    [SerializeField]
    private AudioSource m_ClenseShrine = null; 

   

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

                Loook.CanOff();
                Controll.SetItem(true);

                Clense();
                Destroy(this);


                
            }
        }
    }


    void Clense()
    {
        if(m_ClenseParticle!=null)
        {
            Destroy(Instantiate(m_ClenseParticle, transform.position, transform.rotation), 10.0f);
        }

        if (m_ClenseShrine != null && !m_ClenseShrine.isPlaying)
        {
            m_ClenseShrine.Play(); 
        }
    }

}
