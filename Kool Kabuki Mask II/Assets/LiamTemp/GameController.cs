using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


    private bool m_Pause = false;

    bool m_Item = false;


    int count;

    GameObject m_OldMan;

    QuestObjective questItem;

    private float m_prox = 5;

    private GameObject m_Player;

    // Use this for initialization    
    void Start ()
    {
        count = 0;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_OldMan = GameObject.FindGameObjectWithTag("OLDMAN");
        questItem.GetComponent<QuestObjective>();
    }

    // Update is called once per frame
    void Update()
    {
        LockedIn();
        HaveOrb();
        NextQuest();
        Talk();
    }


    public void SetItem(bool item)
    {
        m_Item = item;
    }


    void LockedIn()
    {
        if(m_Pause)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void HaveOrb()
    {
        if(m_Item)
        {
            count++;

            m_Item = false;
        }
    }

    void NextQuest()
    {
        if(count == 1)
        {
            questItem.m_Next[1] = false;
            questItem.m_Next[2] = true;
        }

        if (count == 2)
        {
            questItem.m_Next[2] = false;
            questItem.m_Next[3] = true;
        }
        if (count == 3)
        {
            questItem.m_Next[3] = false;
            questItem.m_Next[4] = true;
        }

    }


    void Talk()
    {


        bool FirstTalk = false;
        if (Input.GetAxisRaw("Use") > 0)
        {
            if (Vector3.Distance(m_Player.transform.position, m_OldMan.transform.position) <= m_prox)
            {
                if(!FirstTalk)
                {
                    questItem.m_quests[0].enabled = true;

                    FirstTalk = true;
                }

                questItem.NextImage();
            }
        }
    }

}
