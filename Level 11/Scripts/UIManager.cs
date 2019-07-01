using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Level11
{
    public class UIManager : MonoBehaviour
    {

        [Header("Pickup.")]
        [Tooltip("Pickup Canvas and Text")]
        [SerializeField] private GameObject m_PickupObject;
        [SerializeField] private Text m_PickupText;

        [Header("Progress.")]
        [Tooltip("Progress Canvas and Text")]
        [SerializeField] private GameObject m_ProgressObject;
        [SerializeField] private Text m_ProgressText;

        public GameObject ProgressObject
        {
            get { return m_ProgressObject; }
            set { m_ProgressObject = value; }
        }

        public Text ProgressText
        {
            get { return m_ProgressText; }
            set { m_ProgressText = value; }
        }

        private void Awake()
        {
            m_PickupObject.SetActive(false);
        }

        public void EnableUI(string _UiType, string _Text)
        {
            switch (_UiType)
            {
                case "Pickup":
                    m_PickupObject.SetActive(true);
                    m_PickupText.text = _Text;
                    break;

            }
        }

        public void DisableUI(string _UiType)
        {
            switch (_UiType)
            {
                case "Pickup":
                    m_PickupObject.SetActive(false);
                    break;
            }
        }

    }
}

