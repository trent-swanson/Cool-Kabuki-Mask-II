using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenEnemy : EnemyCharacter
{

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
        //Default funtion is remaining still facing original direction till player enters cone of vision
        if (XZDistance(m_startingPos, transform.position) > CLOSE_ENOUGH_TO_NODE)
            MoveTowards(m_startingPos);
        else //Case of in position
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_startingRot, m_rotateSpeed);

    }
}
