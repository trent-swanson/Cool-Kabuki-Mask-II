using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool m_canDamage = false;

    [SerializeField]
    protected float m_attackDamage = 10.0f;

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
        if(tag != other.tag)
        {
            Character otherCharacter = other.gameObject.GetComponent<Character>();
            if(otherCharacter !=null)
            {
                otherCharacter.TakeDamage(m_attackDamage);
            }
        }
    }
}
