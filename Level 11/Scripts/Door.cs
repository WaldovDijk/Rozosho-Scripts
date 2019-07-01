using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level11
{
    public class Door : MonoBehaviour
    {
        enum DoorState
        {
            Closed,
            Open
        }
        private DoorState m_DoorState;

        [SerializeField] private GameObject m_Door;
        [SerializeField] private Transform m_DoorDown, m_DoorUp;

        [SerializeField ]private float m_Speed = 0.5f;

        [SerializeField] private LayerMask m_EnemyLayer;
        private float m_Fraction = 0;


        void Update()
        {
            UpdateDoorState();
        }

        void UpdateDoorState()
        {
            switch (m_DoorState)
            {
                case DoorState.Open:
                    if (m_Fraction < 1)
                    {
                        m_Fraction += m_Speed * Time.deltaTime;
                        m_Door.transform.position = Vector3.Lerp(m_DoorDown.position, m_DoorUp.position, m_Fraction);
                    }
                    break;
                case DoorState.Closed:
                    if (m_Fraction > 0)
                    {
                        m_Fraction -= m_Speed * Time.deltaTime;
                        m_Door.transform.position = Vector3.Lerp(m_DoorDown.position, m_DoorUp.position, m_Fraction);
                    }                    
                    break;
            }


        }

        void OnTriggerEnter(Collider _Other)
        {
            if (_Other.name == "Trigger" || _Other.name == "spine1")
            {
                m_DoorState = DoorState.Open;
            }
        }
        
        void OnTriggerExit(Collider _Other)
        {
            if (_Other.name == "Trigger" || _Other.name == "spine1")
            {
                m_DoorState = DoorState.Closed;
            }
        }

    }

}

