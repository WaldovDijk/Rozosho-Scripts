using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Level11
{
    public class TriggerCheck : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_Event;
        [SerializeField] private bool m_Bomb;
        void OnTriggerEnter(Collider _Other)
        {
            if (_Other.name == "Trigger" && !m_Bomb)
            {
                m_Event.Invoke();
            }
            if (_Other.name == "RobotCore" && m_Bomb)
            {
                m_Event.Invoke();
            }
        }
    }
}

