using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quests : MonoBehaviour
{

    private GameObject m_Player;


    [SerializeField]
    private float m_prox;

    private float m_TurnSpeed = 10.0f;


 	// Use this for initialization
	void Start ()
    {
      
        m_Player = GameObject.FindGameObjectWithTag("Player");

        

	}
	
	// Update is called once per frame
	void Update ()
    {
        look();
	}


    void look()
    {

        Vector3 Direction = m_Player.transform.position - transform.position;

        Direction.Normalize();
        if (Vector3.Distance(m_Player.transform.position, transform.position) <= m_prox)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direction), m_TurnSpeed * Time.deltaTime);
        }
        else
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), m_TurnSpeed * Time.deltaTime);


        }
    }





}
