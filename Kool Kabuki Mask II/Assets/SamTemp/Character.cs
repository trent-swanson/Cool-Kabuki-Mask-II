using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected float m_health = 100.0f;
    protected float m_maxHealth = 0.0f;

    [SerializeField]
    protected GameObject m_deathEffect = null;

    [SerializeField]
    protected float m_forwardSpeed = 10.0f;
    [SerializeField]
    protected float m_rotateSpeed = 10.0f;

    [SerializeField]
    protected GameObject m_weaponObject = null;
    private Weapon m_weaponScript = null;

    protected float m_colliderHeight;
    protected float m_colliderRadius;
    protected Vector3 m_colliderCenter;

    protected int m_environmentMask;

    protected bool m_canAttack = true;

    protected Rigidbody m_rb;
    protected Animator m_animator;

    // Use this for initialization
    protected virtual void Start ()
    {
        m_maxHealth = m_health;

        m_rb = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();

        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        if(capsuleCollider != null)
        {
            m_colliderHeight = capsuleCollider.height;
            m_colliderRadius = capsuleCollider.radius;
            m_colliderCenter = capsuleCollider.center;
        }

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (capsuleCollider != null)
        {
            m_colliderHeight = boxCollider.size.y;
            m_colliderRadius = boxCollider.size.x;
            m_colliderCenter = boxCollider.center;
        }

        m_environmentMask = LayerMask.GetMask("Environment");

        m_weaponScript = m_weaponObject.GetComponent<Weapon>();
        m_weaponScript.SetTag(gameObject);
    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        if (IsDead())
            CharacterDeath();
    }

    public bool IsDead()
    {
        return (m_health <= 0.0f);
    }

    public virtual void TakeDamage(float damage)
    {
        m_health -= damage;
    }

    public void AddHealth(float health)
    {
        m_health += health;
    }

    public void CharacterDeath()
    {
        //Create deatch effect, destroy after 5 seconds
        if(m_deathEffect!=null)
            Destroy(Instantiate(m_deathEffect, transform.position, transform.rotation), 5.0f);

        Destroy(gameObject);
    }

    public void Attack()
    {
        m_animator.SetBool("Attacking", true);
        m_canAttack = false;
        m_weaponScript.EnableDamage();
    }

    private void AddHitsToList(ref List<Character> hitObjects, RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
            Character hitCharacter = hit.collider.gameObject.GetComponent<Character>();
            if (!hitObjects.Contains(hitCharacter))
                hitObjects.Add(hitCharacter);
        }
    }

    public void EnableAttack()
    {
        m_animator.SetBool("Attacking", false);
        m_canAttack = true;
        m_weaponScript.DisableDamage();
    }
}
