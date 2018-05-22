using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestObjective : Quests
{


    public Image[] m_quests;

    [SerializeField]
    private Image[] m_First;

    [SerializeField]
    private Image[] m_Second;

    [SerializeField]
    private Image[] m_Thrid;

    [SerializeField]
    private Image[] m_End;

    private int m_current;

    public bool[] m_Next = new bool[5];


	// Use this for initialization
	void Start ()
    {
        m_current = 0;

        m_Next[0] = true;
        m_Next[1] = false;
        m_Next[2] = false;
        m_Next[3] = false;
        m_Next[4] = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}


    public void NextImage()
    {
        if(m_Next[0])
        {
            if (m_current <= m_quests.Length)
            {
                m_quests[m_current].enabled = false;

                m_quests[m_current + 1].enabled = true;

                ++m_current;
            }
            else if (m_current > m_quests.Length)
            {
                m_quests[m_current].enabled = false;

                m_current = 0;

                m_Next[0] = false;
                m_Next[1] = true;

                m_First[0].enabled = true;

            }
        }

        else if(m_Next[1])
        {
            if (m_current <= m_First.Length)
            {
                m_First[m_current].enabled = false;

                m_First[m_current + 1].enabled = true;

                ++m_current;
            }
            else if (m_current > m_First.Length)
            {
                m_First[m_current].enabled = false;

                m_current = 0;

            }
        }

        else if (m_Next[2])
        {
            if (m_current <= m_Second.Length)
            {
                m_Second[m_current].enabled = false;

                m_Second[m_current + 1].enabled = true;

                ++m_current;
            }
            else if (m_current > m_Second.Length)
            {
                m_Second[m_current].enabled = false;

                m_current = 0;

           

            }
        }

        else if (m_Next[3])
        {
            if (m_current <= m_Thrid.Length)
            {
                m_Thrid[m_current].enabled = false;

                m_Thrid[m_current + 1].enabled = true;

                ++m_current;
            }
            else if (m_current > m_Thrid.Length)
            {
                m_Thrid[m_current].enabled = false;

                m_current = 0;

     
            }
        }

        else if (m_Next[4])
        {
            if (m_current <= m_End.Length)
            {
                m_End[m_current].enabled = false;

                m_End[m_current + 1].enabled = true;

                ++m_current;
            }
            else if (m_current > m_End.Length)
            {
                m_End[m_current].enabled = false;

                m_current = 0;

            }
        }



    }
    
}
