using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace Level02
{
    public class TriggerScript : MonoBehaviour
    {
        enum TriggerType
        {
            LiftButton,
            InsideLift,
            LaunchPad,
            FuelTank,
            Crane,
            EndGame
        }
        [SerializeField] private TriggerType m_TriggerType;
        private UiManager m_UiManager;
        private Lift m_Lift;
        private LaunchPad m_LaunchPad;
        private CraneController m_Crane;
        private Level02_Pickup_Fuell m_FuelTank;
        private GameManager m_GameManager;

        private float m_CrashTimer = 3f;

        private bool m_LiftBool, m_LaunchBool, m_FuelBool, m_CraneBool, m_BeingCarried, m_Crash, m_EndBool;

        private void Awake()
        {
            switch (m_TriggerType)
            {
                case TriggerType.FuelTank:
                    m_FuelTank = this.gameObject.GetComponent<Level02_Pickup_Fuell>();
                    break;
            }
            m_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            m_LaunchPad = GameObject.Find("LaunchPad").GetComponent<LaunchPad>();
            m_Lift = GameObject.Find("Lift").GetComponent<Lift>();
            m_UiManager = GameObject.Find("UI Manager").GetComponent<UiManager>();
            m_Crane = GameObject.Find("Crane").GetComponent<CraneController>();
            m_UiManager.m_Interact.SetActive(false);
            m_UiManager.m_PadControls.SetActive(false);
            m_UiManager.m_CraneControls.SetActive(false);
            m_UiManager.m_CrashUI.SetActive(false);
        }

        private void Update()
        {

            if (m_LiftBool)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    CallElevator();
            }
            if (m_LaunchBool)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    m_LaunchPad.RotateLeft();
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    m_LaunchPad.RotateRight();
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    m_LaunchPad.ThrustUp();
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    m_LaunchPad.ThrustDown();
                }
            }
            if (m_CraneBool)
            {
                    m_Crane.KeyPress(KeyCode.LeftArrow);

                    m_Crane.KeyPress(KeyCode.RightArrow);

            }
            if (m_FuelBool)
            {

                if (Input.GetKeyDown(KeyCode.F) && !m_BeingCarried)
                {
                    m_FuelTank.PickupTank();
                    m_BeingCarried = true;
                }
                if (Input.GetKeyDown(KeyCode.E) && m_BeingCarried)
                {
                    m_FuelTank.DropTank();
                    m_BeingCarried = false;
                }
                /* if (Input.GetKeyDown(KeyCode.Mouse1) && m_BeingCarried)
                {
                    m_FuelTank.ThrowTank();
                    m_BeingCarried = false;
                }
                */
            }

            if (m_EndBool)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    m_GameManager.Progress += 1;
                    Debug.Log("You ended the game");
                }
            }
        }

        // Update is called once per frame
        private void OnTriggerStay(Collider other)
        {
            if (other.name == "Trigger")
            {
                switch (m_TriggerType)
                {
                    case TriggerType.LiftButton:
                        if (m_GameManager.Progress > 0)
                        {
                            m_UiManager.m_Interact.SetActive(true);
                            m_UiManager.m_PickupText.text = "Press E to call the elevator";
                            m_LiftBool = true;
                            
                        }
                        break;
                    case TriggerType.InsideLift:
                        if (m_GameManager.Progress > 0)
                        {
                            m_UiManager.m_Interact.SetActive(true);
                            m_UiManager.m_PickupText.text = "Press E to move the elevator";
                            m_LiftBool = true;
                            if (m_GameManager.Progress == 1)
                            {
                                m_GameManager.Progress += 1;
                                m_GameManager.Crash();
                                m_Crash = true;
                            }

                            if (m_Crash == true)
                            {
                                m_CrashTimer -= Time.deltaTime;
                                if (m_CrashTimer > 0)
                                {
                                    m_UiManager.m_CrashUI.SetActive(true);
                                }
                                else
                                {
                                    m_Crash = false;
                                    m_UiManager.m_CrashUI.SetActive(false);
                                }
                            }
                        }

                        break;
                    case TriggerType.LaunchPad:
                        m_UiManager.m_PadControls.SetActive(true);
                        m_LaunchBool = true;
                        break;
                    case TriggerType.Crane:
                        if (m_GameManager.Progress > 1)
                        {
                            m_UiManager.m_CraneControls.SetActive(true);
                            m_CraneBool = true;
                        }
                        break;
                    case TriggerType.FuelTank:
                        if (m_GameManager.Progress > 2)
                        {
                            if (!m_BeingCarried)
                            {
                                m_UiManager.m_Interact.SetActive(true);
                                m_UiManager.m_PickupText.text = "Press F to pick up fuel tank";
                                m_FuelBool = true;
                            }
                        }
                        
                        break;
                    case TriggerType.EndGame:
                        if (m_GameManager.Progress == 4)
                        {
                            m_UiManager.m_Interact.SetActive(true);
                            m_UiManager.m_PickupText.text = "Press F to end the level";
                            m_EndBool = true;
                        }
                        break;

                }
            }
           
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Trigger")
            {
                switch (m_TriggerType)
                {
                    case TriggerType.LiftButton:
                        m_UiManager.m_Interact.SetActive(false);
                        m_LiftBool = false;
                        break;
                    case TriggerType.InsideLift:
                        m_UiManager.m_Interact.SetActive(false);
                        m_LiftBool = false;
                        break;
                    case TriggerType.LaunchPad:
                        m_UiManager.m_PadControls.SetActive(false);
                        m_LaunchBool = false;
                        break;
                    case TriggerType.Crane:
                        m_UiManager.m_CraneControls.SetActive(false);
                        m_CraneBool = false;
                        break;
                    case TriggerType.FuelTank:
                        m_UiManager.m_Interact.SetActive(false);
                        m_LiftBool = false;
                        break;
                }
            }
        }

        private void CallElevator()
        {
            if (m_Lift.m_Top)
            {
                m_Lift.m_Top = false;
            }
            else
            {
                m_Lift.m_Top = true;
            }     
        }
    }
}

