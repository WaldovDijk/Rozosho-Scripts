using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level11
{
    public class PickupTrigger : MonoBehaviour
    {

            private GameObject m_Camera;
            private BoxCollider m_Collider;
            private Rigidbody m_Rigidbody;

            private void Awake()
            {
                m_Camera = GameObject.Find("Main Camera");
                this.gameObject.layer = 2;
                SetCollider();
                SetRigidbody();
            }

            private void SetCollider()
            {
                m_Collider = GetComponent<BoxCollider>();
                m_Collider.isTrigger = true;
                m_Collider.center = new Vector3(0,0,1f);
                m_Collider.size = new Vector3(0.1f, 0.1f, 2f);
            }

            private void SetRigidbody()
            {
                m_Rigidbody = GetComponent<Rigidbody>();
                m_Rigidbody.useGravity = false;
                m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }

            private void FixedUpdate()
            {
                transform.position = new Vector3(m_Camera.transform.position.x, m_Camera.transform.position.y, m_Camera.transform.position.z);
                transform.rotation = new Quaternion(m_Camera.transform.rotation.x, m_Camera.transform.rotation.y, m_Camera.transform.rotation.z, m_Camera.transform.rotation.w);
            }
        
    }
}

