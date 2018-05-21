using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemy : EnemyCharacter
{
    [SerializeField]
    private float m_panAngle = 100.0f;
    [SerializeField]
    private float m_panningSpeed = 1.0f;

    private bool m_invertRotation = false;

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
        //Default funtion is remaining still looking left to right till player enters cone of vision
        if (XZDistance(m_startingPos, transform.position) > CLOSE_ENOUGH_TO_NODE)
            MoveTowards(m_startingPos);
        else //Case of in position, rotate slowling left and right
        {
            Quaternion lookingDir = m_startingRot;
            if(m_invertRotation)
                lookingDir *= Quaternion.Euler(Vector3.up * (m_panAngle / 2.0f));
            else
                lookingDir *= Quaternion.Euler(Vector3.up * (-m_panAngle / 2.0f));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookingDir, m_panningSpeed);

            if (Quaternion.Angle(lookingDir, transform.rotation) < 1.0f)
                m_invertRotation = !m_invertRotation;
        }
    }
}
