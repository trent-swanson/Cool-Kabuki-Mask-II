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

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();

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

        //Attacking
        if (m_canAttack && Input.GetAxisRaw("Fire1") > 0.0f)
            Attack(m_enemyMask);

    }

    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, -transform.up, m_colliderHeight+ 0.1f, m_environmentMask))
            return true;
        return false;
    }

    public bool CanHitPlayer(Vector3 enemyPosition)
    {
        //TODO
        //Check if player is blocking
        return true;
        //Check funcky angle
    }
}
