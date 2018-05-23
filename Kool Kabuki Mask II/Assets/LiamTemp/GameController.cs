using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


    private bool m_Pause = false;

    bool m_Item = false;

    bool m_keyUp;

    int count;

    bool FirstTalk = false;

    GameObject m_OldMan;

	[SerializeField]
    QuestObjective questItem;


    private List<GameObject> m_AllEnemies;

    private float m_prox = 5;

    [SerializeField]
    private float m_EProx;

    private GameObject m_Player;

    // Use this for initialization    
    void Start ()
    {
        count = 0;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_OldMan = GameObject.FindGameObjectWithTag("OLDMAN");
        m_AllEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));


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
      
        if (count == 3)
        {
            questItem.m_Next[1] = false;
            questItem.m_Next[2] = true;
        }

    }


    void Talk()
    {


        
        if (Input.GetAxisRaw("Use") > 0)
        {
            if (Vector3.Distance(m_Player.transform.position, m_OldMan.transform.position) <= m_prox)
            {
                if (m_keyUp)
                {
                    m_keyUp = false;
                    if (questItem.m_quests[0].enabled == true)
                    {
                        questItem.m_quests[0].enabled = false;
                    }

                    if (!FirstTalk)
                    {
                        questItem.m_quests[0].enabled = true;

                        FirstTalk = true;

                        questItem.m_Paused = true;
                    }



                    if (questItem.m_quests[0].enabled == false)
                    {
                        questItem.NextImage();
                    }
                   
                    
                }

            }
        }
        if (Input.GetAxisRaw("Use") == 0)
        {
          if (!m_keyUp)
          {
              m_keyUp = true;
          }
        }
    }

    void EnemyProx()
    {
        foreach(GameObject obj in m_AllEnemies)
        {
            if (Vector3.Distance(m_Player.transform.position, obj.transform.position) <= m_prox)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }

}
