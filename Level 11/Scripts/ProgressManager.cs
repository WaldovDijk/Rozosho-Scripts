using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace Level11
{
    public class ProgressManager : MonoBehaviour
    {


        private UIManager m_UiManager;
        private PoolManager m_PoolManager;
        private int m_Progress;
        [SerializeField] private string[] m_Messages;
        public int Progress
        {
            get { return m_Progress; }
            set { m_Progress = value; }
        }

        public float m_Timer = 5f;
        public VisualEffect m_Explosion;

        [SerializeField] private GameObject m_NormalWall, m_BreachWall;
        [SerializeField] private GameObject m_NormalDoor, m_BreachDoor;

        [SerializeField] private GameObject m_Bomb, m_BombPos;

        void Awake()
        {
            m_PoolManager = gameObject.GetComponent<PoolManager>();
            m_UiManager = gameObject.GetComponent<UIManager>();
            m_NormalWall.SetActive(true);
            m_BreachWall.SetActive(false);
            
        }
        void Update()
        {
            SetProgressUI();

            switch (Progress)
            {
                case 2:
                    m_NormalWall.SetActive(false);
                    m_BreachWall.SetActive(true);
                    SpawnZombies();
                    break;
                case 3:
                    break;
                case 4:
                    if (m_Timer > 0f)
                        m_Timer -= Time.deltaTime;

                    m_Bomb.transform.position = m_BombPos.transform.position;
                    m_Bomb.GetComponent<Shield>().DropShield();

                    if (m_Timer <= 0f)
                    {

                        m_Explosion.gameObject.SetActive(true);
                        m_NormalDoor.SetActive(false);
                        m_BreachDoor.SetActive(true);
                        m_Bomb.SetActive(false);
                    }
                    
                    break;
                    
            }
        }

        public void AddProgress(int _ProgressCount)
        {
            if(m_Progress == _ProgressCount)
            m_Progress += 1;
        }

        private void SpawnZombies()
        {
            m_PoolManager.m_ZombieSpawning = true;
        }


        void SetProgressUI()
        {
            m_UiManager.ProgressText.text = m_Messages[m_Progress];
        }

    }
}

