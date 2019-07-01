using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace Level02
{
    public class LaunchPad : MonoBehaviour
    {

        [SerializeField] private Material m_ReadyMaterial, m_Material;
        [SerializeField] private VisualEffect m_EngineSmoke;
        [SerializeField] private GameManager m_GameManager;
        [SerializeField] private GameObject m_Pad;
        
        //Particles
        private Vector3 m_Speed;
        private float m_SpawnRate;

        [SerializeField] private LayerMask m_Layer;

       

        private void Awake()
        {
            m_EngineSmoke.SetVector3("Max Velocity", new Vector3(0, 0, 0));
            m_EngineSmoke.SetFloat("SpawnRate", 0f);
            m_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        private void Update()
        {
            m_EngineSmoke.SetVector3("Max Velocity", m_Speed);
            m_EngineSmoke.SetFloat("SpawnRate", m_SpawnRate);

            ChangeMaterial();
        }

        private void ChangeMaterial()
        {
            if (m_Speed.x == 25f &&
                gameObject.transform.rotation.y < 0.1f &&
                gameObject.transform.rotation.y > -0.1f)
            {
                m_Pad.gameObject.GetComponent<Renderer>().material = m_ReadyMaterial;
                if (m_GameManager.Progress == 0)
                {
                    m_GameManager.Progress += 1;
                }
            }
            else
            {
                m_Pad.gameObject.GetComponent<Renderer>().material = m_Material;
            }

        }

        public void ThrustUp()
        {
            if (m_Speed.x < 25f)
            {
                m_Speed.x += 0.25f;
                m_SpawnRate += 0.5f;
            }
        }
        public void ThrustDown()
        {
            if (m_Speed.x >= 0.25f)
            {
                m_Speed.x -= 0.25f;
                m_SpawnRate -= 0.5f;
            }
        }

        public void RotateLeft()
        {
            gameObject.transform.Rotate(0,20 * Time.deltaTime,0);
        }
        public void RotateRight()
        {
            gameObject.transform.Rotate(0, -20 * Time.deltaTime, 0);
        }

        void OnTriggerStay(Collider other)
        {
            if (((1 << other.gameObject.layer) & m_Layer) != 0 && m_Speed.x == 25f)
            {
                other.gameObject.GetComponent<DamageablePart>().Damage(10);
            }
        }
    }
}

