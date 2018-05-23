using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool m_canDamage = false;

    [SerializeField]
    protected float m_attackDamage = 10.0f;

    [SerializeField]
    private AudioSource m_woodHit = null;

    public void SetTag(GameObject parent)
    {
        tag = parent.tag;
    }

    public void EnableDamage()
    {
        m_canDamage = true;
    }

    public void DisableDamage()
    {
        m_canDamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(tag != other.tag && m_canDamage)
        {
            Character otherCharacter = other.gameObject.GetComponent<Character>();
            if(otherCharacter !=null)
            {
                otherCharacter.TakeDamage(m_attackDamage);
                if(m_woodHit!=null && !m_woodHit.isPlaying)
                    m_woodHit.Play();
            }
        }
    }
}
