using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraY : MonoBehaviour
{
    [Tooltip("Lock the camera's Z position to this value")]
    public float m_YPosition = 1;
    public float m_XPosition = 1;
    public float m_ZPosition = 1;

    public void LockCam()
    {
        m_YPosition = Camera.main.transform.localPosition.y;
        m_XPosition = Camera.main.transform.localPosition.x;
        m_ZPosition = Camera.main.transform.localPosition.z;
    }
}

