using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacter : Character
{
    [SerializeField]
    private float m_strafingSpeed = 10.0f;
    [SerializeField]
    private float m_jumpingSpeed = 10.0f;
    [SerializeField]
    private float m_jumpingBuffer = 0.05f;

    [SerializeField]
    private float m_jumpLandingDistance = 0.5f;

    [SerializeField]
    private float m_healthRegenPerSecond = 5.0f;
    [SerializeField]
    private float m_healthRegenTime = 3.0f;
    private float m_healthRegenTimer = 0.0f;

    [SerializeField]
    private float m_attackingTime = 1.0f;
    private float m_attackingTimer = 0.0f;

    enum PLAYER_STATE {IDLE, ATTACKING, BLOCKING, IN_AIR};
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

        if(m_playerState == PLAYER_STATE.ATTACKING)
        {
            //Move forwards for limited time
            m_attackingTimer += Time.deltaTime;
            if (m_attackingTimer > m_attackingTime)
                m_playerState = PLAYER_STATE.IDLE;

            //automatically move player forward
            Vector3 velocity = Vector3.zero;
            velocity += transform.forward * m_forwardSpeed;
            velocity.y = m_rb.velocity.y;
            m_rb.velocity = velocity;

            //Roatation
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X") * m_rotateSpeed, 0.0f));
        }
        //When blocking dont allow any movement
        else if (Input.GetAxisRaw("Block") > 0.0f && m_playerState !=PLAYER_STATE.IN_AIR)
            m_playerState = PLAYER_STATE.BLOCKING;
        else
        {
            //On attack, force player forawrds with just rotation control
            if (m_canAttack && Input.GetAxisRaw("Attack") > 0.0f && m_playerState != PLAYER_STATE.IN_AIR)
            {
                m_playerState = PLAYER_STATE.ATTACKING;
                Attack();
                m_attackingTimer = 0.0f;
            }
            else
            {
                if (m_playerState == PLAYER_STATE.IN_AIR)
                {
                    //Check for landing
                    if (Physics.Raycast(transform.position + m_colliderCenter, -transform.up, m_colliderHeight + m_jumpLandingDistance, m_environmentMask))
                    {
                        m_animator.SetTrigger("JumpLanding");
                        m_playerState = PLAYER_STATE.IDLE;
                    }  
                }
                else
                    m_playerState = PLAYER_STATE.IDLE;

                Vector3 velocity = Vector3.zero;

                //Forward
                velocity += transform.forward * Input.GetAxisRaw("Vertical") * m_forwardSpeed;

                //strafe
                velocity += transform.right * Input.GetAxisRaw("Horizontal") * m_strafingSpeed;

                //Default to current y val unless jumping
                if (Input.GetAxisRaw("Jump") > 0.0f && IsGrounded())
                {
                    velocity.y = m_jumpingSpeed;
                    m_animator.SetTrigger("Jump");
                }
                else
                    velocity.y = m_rb.velocity.y;

                //Roatation
                transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X") * m_rotateSpeed, 0.0f));

                //Set veleocity 
                m_rb.velocity = velocity;
            }
        }

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
        if (Physics.Raycast(transform.position + m_colliderCenter, -transform.up, m_colliderHeight + m_jumpingBuffer, m_environmentMask))
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

    public override void EnableAttack()
    {
        m_animator.SetBool("Attacking", false);
        m_canAttack = true;
        m_weaponScript.DisableDamage();
        m_playerState = PLAYER_STATE.IDLE;
    }

    public float GetHealthPercent()
    {
        return m_health / m_maxHealth;
    }
}
