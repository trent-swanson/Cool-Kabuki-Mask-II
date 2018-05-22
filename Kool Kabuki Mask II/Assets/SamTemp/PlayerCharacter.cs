using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacter : Character
{
    [SerializeField]
    protected float m_strafingSpeed = 10.0f;
    [SerializeField]
    protected float m_jumpingSpeed = 10.0f;
    [SerializeField]
    protected float m_jumpingBuffer = 0.05f;

    [SerializeField]
    protected float m_healthRegenPerSecond = 5.0f;
    [SerializeField]
    protected float m_healthRegenTime = 3.0f;
    private float m_healthRegenTimer = 0.0f;
    enum PLAYER_STATE {IDLE, ATTACKING, BLOCKING};
    private PLAYER_STATE m_playerState = PLAYER_STATE.IDLE; 

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();

        //Movement, attacking, blocking
        Vector3 velocity = Vector3.zero;

        //Forward
        velocity += transform.forward * Input.GetAxisRaw("Vertical") * m_forwardSpeed;

        //strafe
        velocity += transform.right * Input.GetAxisRaw("Horizontal") * m_strafingSpeed;

        //Default to current y val unless jumping
        if (Input.GetAxisRaw("Jump") > 0.0f && IsGrounded())
            velocity.y = m_jumpingSpeed;
        else
            velocity.y = m_rb.velocity.y;

        //Roatation
        transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X") * m_rotateSpeed, 0.0f));

        //Set veleocity 
        m_rb.velocity = velocity;

        //Attacking/Blocking
        if (Input.GetAxisRaw("Block") > 0.0f)
            m_playerState = PLAYER_STATE.BLOCKING;
        else if (m_canAttack && Input.GetAxisRaw("Attack") > 0.0f)
        {
            m_playerState = PLAYER_STATE.ATTACKING;
            Attack(m_enemyMask);
        }
        else
            m_playerState = PLAYER_STATE.IDLE;

        //Health Regen
        if(m_health != m_maxHealth)
        {
            m_healthRegenTimer += Time.deltaTime;
            if (m_healthRegenTimer > m_healthRegenTime)
                m_health += m_healthRegenPerSecond * Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position + m_colliderCenter, -transform.up, m_colliderHeight/2.0f + m_jumpingBuffer, m_environmentMask))
            return true;
        return false;
    }

    public override void TakeDamage(float damage)
    {
        if (m_playerState != PLAYER_STATE.BLOCKING)
        {
            m_health -= damage;
            m_healthRegenTimer = 0.0f;
        }
    }

    public float GetHealthPercent()
    {
        return m_health / m_maxHealth;
    }
}
