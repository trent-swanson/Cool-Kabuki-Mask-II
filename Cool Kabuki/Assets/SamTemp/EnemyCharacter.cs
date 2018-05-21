using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    protected const float CLOSE_ENOUGH_TO_NODE = 0.5f;
    protected enum ENEMY_STATE { ENEMY_FUNCTION, ATTACKING, INVESTIGATING };
    protected ENEMY_STATE m_currentState = ENEMY_STATE.ENEMY_FUNCTION;

    protected Vector3 m_startingPos;
    protected Quaternion m_startingRot;

    [SerializeField]
    protected float m_detectionRange = 10.0f;

    protected GameObject m_player = null;
    [SerializeField]
    protected Vector3 m_lastKnowPos = Vector3.positiveInfinity;
    protected bool m_isPositionSet = false;

    protected override void Start()
    {
        base.Start();
        m_startingPos = transform.position;
        m_startingRot = transform.rotation;

        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (m_player == null)
            return;

        //Setting states
        if (m_currentState == ENEMY_STATE.ATTACKING && Vector3.Distance(m_player.transform.position, transform.position) > m_detectionRange)
            m_currentState = ENEMY_STATE.INVESTIGATING;
        else if (Vector3.Distance(m_player.transform.position, transform.position) > m_detectionRange)
            m_currentState = ENEMY_STATE.ENEMY_FUNCTION;
        else
            m_currentState = ENEMY_STATE.ATTACKING;

        //Runing states
        switch (m_currentState)
        {
            case ENEMY_STATE.ENEMY_FUNCTION:
                EnemyFunctions();
                break;
            case ENEMY_STATE.ATTACKING:
                AttackPlayer();
                break;
            case ENEMY_STATE.INVESTIGATING:
                Investigate();
                break;
            default:
                break;
        }

    }

    protected void MoveTowards(Vector3 pos)
    {
        m_rb.velocity = transform.forward * m_forwardSpeed;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((pos - transform.position).normalized), m_rotateSpeed * Time.deltaTime);
    }

    //Move towards player 
    //than attack when close enouygh
    protected void AttackPlayer()
    {
        //When just close enough to attack 
        if (m_canAttack && Vector3.Distance(m_player.transform.position, transform.position) < m_attackRange * 0.9f + m_colliderExtents.z)
        {
            transform.LookAt(m_player.transform.position);
            Attack(m_playerMask);
        }
        else
        {
            MoveTowards(m_player.transform.position);
        }
    }

    protected void Investigate()
    {
        //Set last known pos
        if(!m_isPositionSet)
        {
            m_isPositionSet = true;
            m_lastKnowPos = m_player.transform.position;
        }

        //Move towards player
        MoveTowards(m_lastKnowPos);

        if(Vector3.Distance(m_lastKnowPos, transform.position) < CLOSE_ENOUGH_TO_NODE)
        {
            m_currentState = ENEMY_STATE.ENEMY_FUNCTION;
            m_isPositionSet = false;
        }
    }

    protected virtual void EnemyFunctions()
    {

    }
}
