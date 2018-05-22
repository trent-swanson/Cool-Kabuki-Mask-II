using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{

    [SerializeField]
    private float m_ViewRangeDown = -0.90f;

    [SerializeField]
    private float m_ViewRangeUp = 0.90f;

    [SerializeField]
    private float sensitivityY = 15F;


    private Vector3 offset;

    Transform m_CamT;


    // Use this for initialization
    void Start ()
    {
        m_CamT = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {

        float m_RotationY = Input.GetAxisRaw("Mouse Y") * sensitivityY;


        Quaternion deltaRotation = Quaternion.Euler(-m_RotationY, 0, 0);
        Quaternion currentRotation = m_CamT.localRotation;


        m_CamT.localRotation *= Quaternion.Euler(-m_RotationY, 0, 0);
        if (Vector3.Dot(transform.forward, Vector3.up) < m_ViewRangeDown)
        {
            m_CamT.localRotation = currentRotation;
        }
        if (Vector3.Dot(transform.forward, Vector3.up) > 0.9)
        {
            m_CamT.localRotation = currentRotation;
        }

        


    }

}
