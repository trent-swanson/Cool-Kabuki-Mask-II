using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


    private bool m_Pause = false;

	// Use this for initialization
	void Start ()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LockedIn();
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

}
