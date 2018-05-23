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
    CanvasLook Loook;

    private int m_current;

    public bool[] m_Next = new bool[3];

    public bool m_Paused = false;


	// Use this for initialization
	void Start()
    {
        m_current = 0;

        m_Next[0] = true;
        m_Next[1] = false;
        m_Next[2] = false;
       
    }
	
	// Update is called once per frame
	void Update ()
    {
      
	}


    public void NextImage()
    {
        if(m_Next[0])
        {
            
            if (m_current < m_quests.Length)
            {
               

                m_quests[m_current].enabled = false;

                if (m_current < (m_quests.Length - 1))
                    m_quests[m_current + 1].enabled = true;
                
                ++m_current;

                if (m_current >= m_quests.Length)
                {
                    m_Paused = false;
                    Loook.CanOn();
                }
            }
            else if(m_current >= m_quests.Length)
            {
                --m_current;
                m_quests[m_current].enabled = false;

                m_current = 0;
                Loook.CanOn();

                m_Next[0] = false;
                m_Next[1] = true;
            }
        }

        if (m_Next[1])
        {
            if (m_current < m_First.Length)
            {
                if (m_First[0].enabled == false)
                {
                    m_First[0].enabled = true;
                    m_Paused = true;

                }
                else
                {
                    m_First[m_current].enabled = false;
                }

                if (m_current < (m_First.Length - 1))
                    m_First[m_current + 1].enabled = true;

                

                ++m_current;
            }
            else if (m_current >= m_First.Length)
            {
                --m_current;
                m_First[m_current].enabled = false;

                m_current = 0;
                m_Paused = false;
                Loook.CanOn();

            }
        }

        if (m_Next[2])
        {
            if (m_current < m_Second.Length)
            {
                if (m_Second[0].enabled == false)
                {
                    m_Second[0].enabled = true;

                    m_Paused = true;

                    if(m_Next[1] == true)
                    {
                        m_Next[1] = false;
                    }

                }
                else
                {
                    m_Second[m_current].enabled = false;
                }

                if (m_current < (m_Second.Length - 1))
                    m_Second[m_current + 1].enabled = true;

               

                ++m_current;
            }
            else if (m_current >= m_Second.Length)
            {

                --m_current;

                m_Second[m_current].enabled = false;

                m_current = 0;
                Loook.CanOn();
                m_Paused = false;

            }
        }

    }   
}
