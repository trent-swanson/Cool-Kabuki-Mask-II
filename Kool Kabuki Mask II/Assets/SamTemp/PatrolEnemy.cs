using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyCharacter
{
    [SerializeField]
    private List<Transform> m_patrolNodes = new List<Transform>();
    private int nodeIndex = 0;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyFunctions()
    {
        if (Vector3.Distance(m_patrolNodes[nodeIndex].position, transform.position) < CLOSE_ENOUGH_TO_NODE)
        {
            nodeIndex++;
            if (nodeIndex == m_patrolNodes.Count)
                nodeIndex = 0;
        }

        MoveTowards(m_patrolNodes[nodeIndex].position);
    }
}
