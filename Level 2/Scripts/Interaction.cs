using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level02
{
    public class Interaction : MonoBehaviour
    {

            private GameObject m_Player;
            private BoxCollider m_Collider;
            private Rigidbody m_Rigidbody;

            private void Awake()
            {
                m_Player = GameObject.Find("Player");
                this.gameObject.layer = 2;
                SetCollider();
                SetRigidbody();
            }

            private void SetCollider()
            {
                m_Collider = GetComponent<BoxCollider>();
                m_Collider.isTrigger = true;
                m_Collider.size = new Vector3(1f, 2f, 1f);
            }

            private void SetRigidbody()
            {
                m_Rigidbody = GetComponent<Rigidbody>();
                m_Rigidbody.useGravity = false;
                m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }

            private void FixedUpdate()
            {
                transform.position = new Vector3(m_Player.transform.position.x, m_Player.transform.position.y + 1f, m_Player.transform.position.z);
            }
        
    }
}

