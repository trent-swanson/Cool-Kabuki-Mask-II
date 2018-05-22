using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {

    public Image m_scratchImage;

    private PlayerCharacter m_playerCharacter = null;

	// Use this for initialization
	void Start ()
    {
        m_playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_playerCharacter!= null)
        {
            Color scratchColour = m_scratchImage.color;
            scratchColour.a = 1 - m_playerCharacter.GetHealthPercent();
            m_scratchImage.color = scratchColour;
        }
    }
}
