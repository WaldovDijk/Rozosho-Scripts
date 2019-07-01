using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Level11
{
    public class PickupCheck : MonoBehaviour
    {
        [SerializeField] private ScriptablePickup m_Pickup;
        [SerializeField] private UnityEvent m_Event;

        //Managers Needed
        private UIManager m_UiManager;
        private ProgressManager m_ProgressManager;

        private bool m_UIOn = true;

        public bool UIOn
        {
            set { m_UIOn = value; }
        }

        void Awake()
        {
            m_UiManager = GameObject.Find("Full Level").GetComponent<UIManager>();
            m_ProgressManager = GameObject.Find("Full Level").GetComponent<ProgressManager>();
        }

        void Update()
        {
            if (m_Pickup.PressKey)
            {
                if (Input.GetKeyDown(m_Pickup.Key))
                {
                    m_Event.Invoke();
                }
            }
        }
        
        void OnTriggerStay(Collider _Other)
        {
            //SetUI
            if (_Other.name == "PickupTrigger")
            {
                if (m_Pickup.ProgressNeeded <= m_ProgressManager.Progress)
                {
                    if (m_UIOn)
                    {
                        m_UiManager.EnableUI("Pickup", m_Pickup.Text);
                    }
                    else
                    {
                        m_UiManager.DisableUI("Pickup");
                    }
                    m_Pickup.PressKey = true;
                }
            }
        }

        void OnTriggerExit(Collider _Other)
        {
            //DisableUI
            if (_Other.name == "PickupTrigger")
            {
                if (m_Pickup.ProgressNeeded <= m_ProgressManager.Progress)
                {
                    m_UiManager.DisableUI("Pickup");
                    m_Pickup.PressKey = false;
                }
            }
        }

        
    }
}

