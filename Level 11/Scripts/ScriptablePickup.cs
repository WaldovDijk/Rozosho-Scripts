using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Level11
{
    [CreateAssetMenu(fileName = "Pickup", menuName = "Level11/Triggers/Pickup")]
    public class ScriptablePickup : ScriptableObject
    {


        [SerializeField] private string m_Text;

        [SerializeField]private KeyCode m_Key;

        [SerializeField] private int m_ProgressNeeded;

        private bool m_PressKey = false;

        public int ProgressNeeded
        {
            get { return m_ProgressNeeded; }
            set { m_ProgressNeeded = value; }
        }

        public string Text
        {
            get { return m_Text; }
        }

        public KeyCode Key
        {
            get { return m_Key; }
        }

        public bool PressKey
        {
            get { return m_PressKey; }
            set { m_PressKey = value; }
        }
    }


}
