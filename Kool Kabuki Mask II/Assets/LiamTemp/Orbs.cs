using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{
    private GameObject m_Orb;

    private float m_prox = 2;



    QuestObjective objectsQ;

    bool m_Dead = false;

   

    // Use this for initialization
    void Start ()
    {
        m_Orb = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        TurnOn();
        TurnOff();
	}



    void TurnOn()
    {
        if(objectsQ.m_Next[0] == false)
        {
               gameObject.SetActive(true);
        }
    }

    void TurnOff()
    {
        if (Vector3.Distance(m_Orb.transform.position, transform.position) <= m_prox)
        {
            if(m_Dead)
            {
                Destroy(this);
            }
        }
    }


    public void SetDead(bool dead)
    {
        m_Dead = dead;
    }
}
