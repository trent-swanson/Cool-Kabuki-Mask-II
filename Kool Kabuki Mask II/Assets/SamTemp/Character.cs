using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected float m_health = 100.0f;
    protected float m_maxHealth = 0.0f;

    [SerializeField]
    protected float m_attackDamage = 10.0f;

    [SerializeField]
    protected GameObject m_deathEffect = null;

    [SerializeField]
    protected float m_forwardSpeed = 10.0f;
    [SerializeField]
    protected float m_rotateSpeed = 10.0f;

    [SerializeField]
    protected float m_attackRange = 1.0f;

    protected Vector3 m_colliderExtents;

    protected int m_enviromentMask;
    protected int m_enemyMask;
    protected int m_playerMask;

    protected bool m_canAttack = true;

    protected Rigidbody m_rb;
    protected Animator m_animator;

    // Use this for initialization
    protected virtual void Start ()
    {
        m_maxHealth = m_health;

        m_rb = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();

        m_colliderExtents = GetComponent<Collider>().bounds.extents;

        m_enviromentMask = LayerMask.GetMask("Enviroment");
        m_enemyMask = LayerMask.GetMask("Enemy");
        m_playerMask = LayerMask.GetMask("Player");
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

    public void TakeDamage(float damage)
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

    public void Attack(int mask)
    {
        m_animator.SetBool("Attacking", true);
        m_canAttack = false;
        List<Character> hitObjects = new List<Character>();

        RaycastHit[] hits;
        //Right
        hits = Physics.RaycastAll(transform.position + transform.right * m_colliderExtents.x, transform.forward, m_colliderExtents.z + m_attackRange, mask);
        AddHitsToList(ref hitObjects, hits);
        //Center
        hits = Physics.RaycastAll(transform.position, transform.forward, m_colliderExtents.z + m_attackRange, mask);
        AddHitsToList(ref hitObjects, hits);
        //Left
        hits = Physics.RaycastAll(transform.position - transform.right * m_colliderExtents.x, transform.forward, m_colliderExtents.z + m_attackRange, mask);
        AddHitsToList(ref hitObjects, hits);

        foreach (Character character in hitObjects)
        {
            character.TakeDamage(m_attackDamage);
        }
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
    }
}
