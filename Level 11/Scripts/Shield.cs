using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Level11
{
    public class Shield : MonoBehaviour
    {
        enum ShieldState
        {
            OnGround,
            PickedUp
        }
        private ShieldState m_State = ShieldState.OnGround;

        [SerializeField]private Transform m_PlayerCam;

        private bool m_PickedUp;
        private PickupCheck m_PickupCheck;

        void Awake()
        {
            m_PickupCheck = gameObject.GetComponent<PickupCheck>();
        }

        public void ChangeState()
        {
            switch (m_State)
            {
                case ShieldState.OnGround:
                    PickupShield();
                    break;
                case ShieldState.PickedUp:
                    DropShield();
                    break;
            }
        }

        public void DropShield()
        {
            GetComponent<Rigidbody>().isKinematic = false;
            gameObject.transform.parent = null;
            m_State = ShieldState.OnGround;
            m_PickupCheck.UIOn = true;
        }

        public void PickupShield()
        {
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.parent = m_PlayerCam;
            m_State = ShieldState.PickedUp;
            m_PickupCheck.UIOn = false;
           
        }
    }
}

