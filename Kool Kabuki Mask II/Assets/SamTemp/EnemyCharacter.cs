using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    protected const float CLOSE_ENOUGH_TO_NODE = 1.5f;
    protected enum ENEMY_STATE { ENEMY_FUNCTION, ATTACKING, INVESTIGATING, ALERTED };
    [SerializeField]
    protected ENEMY_STATE m_currentState = ENEMY_STATE.ENEMY_FUNCTION;

    [SerializeField]
    protected List<GameObject> m_allies = new List<GameObject>();
    private List<EnemyCharacter> m_enemyCharacters = new List<EnemyCharacter>();

    protected Vector3 m_startingPos;
    protected Quaternion m_startingRot;

    [SerializeField]
    protected float m_detectionRange = 10.0f;
    //In degrees
    [SerializeField]
    protected float m_detectionCone = 30.0f;

    protected GameObject m_player = null;

    [SerializeField]
    protected Vector3 m_lastKnowPos = Vector3.zero;
    protected bool m_isPositionSet = false;

    protected override void Start()
    {
        base.Start();
        m_startingPos = transform.position;
        m_startingRot = transform.rotation;

        m_player = GameObject.FindGameObjectWithTag("Player");

        foreach (GameObject ally in m_allies)
        {
            EnemyCharacter enemyCharacter = ally.GetComponent<EnemyCharacter>();
            if (enemyCharacter != null)
                m_enemyCharacters.Add(enemyCharacter);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //Ensure player is in game
        if (m_player == null)
            return;

        //Check player is close enough for activation

        //Setting states so much nicer than player states. Many switches, much wow
        switch (m_currentState)
        {
            case ENEMY_STATE.ENEMY_FUNCTION:
                if (PlayerInVisionCone())
                    m_currentState = ENEMY_STATE.ATTACKING;
                else if (AllyAttacking())
                    m_currentState = ENEMY_STATE.ALERTED;
                break;

            case ENEMY_STATE.ATTACKING:
                //investigate when player runs away
                if(XZDistance(m_player.transform.position, transform.position) > m_detectionRange)
                    m_currentState = ENEMY_STATE.INVESTIGATING;
                break;

            case ENEMY_STATE.INVESTIGATING:
                //Start attacking when seeing player
                if (PlayerInVisionCone())
                    m_currentState = ENEMY_STATE.ATTACKING;
                else if (AllyAttacking())
                    m_currentState = ENEMY_STATE.ALERTED;
                //When area is investigated move back to orginal behaviour
                else if (XZDistance(m_lastKnowPos, transform.position) < CLOSE_ENOUGH_TO_NODE)
                {
                    m_currentState = ENEMY_STATE.ENEMY_FUNCTION;
                    m_isPositionSet = false; //used in reseting state
                }
                break;

            case ENEMY_STATE.ALERTED:
                //Attack when close enough
                if(XZDistance(m_player.transform.position, transform.position) < m_detectionRange)
                    m_currentState = ENEMY_STATE.ATTACKING;
                //Go investigate when all allys are no longer attacking
                if(!AllyAttacking())
                    m_currentState = ENEMY_STATE.INVESTIGATING;
                break;

            default:
                m_currentState = ENEMY_STATE.ENEMY_FUNCTION;
                break;

        }

        //Runing current state
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
            case ENEMY_STATE.ALERTED:
                Alerted();
                break;
            default:
                break;
        }
    }

    public override void TakeDamage(float damage)
    {
        m_health -= damage;
        m_currentState = ENEMY_STATE.ATTACKING;
    }

    protected void MoveTowards(Vector3 pos)
    {
        Vector3 targetVelocity = transform.forward * m_forwardSpeed;
        targetVelocity.y = m_rb.velocity.y;
        m_rb.velocity = targetVelocity;

        Vector3 targetDirection = pos - transform.position;
        targetDirection.y = 0;
        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, m_rotateSpeed);
    }

    //Move towards player 
    //than attack when close enouygh
    protected void AttackPlayer()
    {
        //When just close enough to attack 
        if (m_canAttack && XZDistance(m_player.transform.position, transform.position) < 0.5f + m_colliderRadius)
        {
            Vector3 targetDirection = m_player.transform.position - transform.position;
            targetDirection.y = 0;
            Quaternion rotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, float.PositiveInfinity);
            Attack();
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
    }

    protected void Alerted()
    {
        MoveTowards(m_player.transform.position);
    }

    private bool AllyAttacking()
    {
        for (int i = 0; i < m_enemyCharacters.Count; i++)
        {
            if (m_enemyCharacters[i] == null)
            {
                m_enemyCharacters.RemoveAt(i);
                i--;
            }
            else
            {
                if(m_enemyCharacters[i].AttackingPlayer())
                    return true;
            }
        }

        return false;
    }

    public bool AttackingPlayer()
    {
        return (m_currentState == ENEMY_STATE.ATTACKING);
    }

    protected virtual void EnemyFunctions()
    {

    }

    protected bool PlayerInVisionCone()
    {
        //Intially check distance
        if (XZDistance(m_player.transform.position, transform.position) > m_detectionRange)
            return false;
        //Check angle
        if(Vector3.Angle(transform.forward, m_player.transform.position - transform.position) > m_detectionCone)
            return false;
        return true;
    }

    protected float XZDistance(Vector3 a, Vector3 b)
    {
        a.y = 0;
        b.y = 0;
        return (Vector3.Distance(a, b));
    }
}
