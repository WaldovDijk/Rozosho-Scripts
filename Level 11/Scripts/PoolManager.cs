using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Level11
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        private List<PoolingObject> m_PoolObjects;

        [SerializeField]
        private List<GameObject> m_Zombies;
        public List<GameObject> Zombie
        {
            get { return m_Zombies; }
        }

        GameObject m_PoolName;

        [SerializeField]
        private GameObject m_Spawn;

        [SerializeField]
        private Transform m_Player;
        private EnemyWeaponPickupBehaviour m_Patrol;
        private BasicChaseTargetState m_Chase;

        private GameObject m_Behaviour;

        private NavMeshAgent m_Agent;

        private float m_CurrentTime, m_StandardTime = 5f;

        public bool m_ZombieSpawning = false;

        void Start()
        {
            m_CurrentTime = m_StandardTime;
            //m_PoolObjects = new List<PoolingObject>();
            for (int p = 0; p < m_PoolObjects.Count; p++)
            {
                m_PoolName = new GameObject("Pool" + " " + m_PoolObjects[p].ObjectType.name);
                m_PoolName.transform.parent = gameObject.transform;
                for (int i = 0; i < m_PoolObjects[p].Amount; i++)
                {
                    if (m_PoolObjects[p].ObjectType.name == "Zombie")
                    {
                        GameObject obj = (GameObject)Instantiate(m_PoolObjects[p].ObjectType);
                        obj.transform.parent = m_PoolName.transform;
                        obj.SetActive(false);
                        m_Zombies.Add(obj);
                    }
                }
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {

                SpawnZombies();
            }

            if (m_ZombieSpawning)
            {
                m_CurrentTime -= Time.deltaTime;

                if (m_CurrentTime <= 0f)
                {
                    SpawnZombies();
                    m_CurrentTime = m_StandardTime;
                }
            }
            


            for (int z = 0; z < m_Zombies.Count; z++)
            {
                if (m_Zombies[z].activeInHierarchy)
                {

                        m_Behaviour = m_Zombies[z].transform.GetChild(0).gameObject;
                        float Distance = Vector3.Distance(m_Player.transform.position, m_Zombies[z].transform.GetChild(0).position);
                        m_Patrol = m_Behaviour.GetComponent<EnemyWeaponPickupBehaviour>();
                        if (m_Patrol.enabled && Distance > 3)
                        {
                            //m_Patrol.NavMeshAgent.destination = m_Player.transform.position;
                            // m_Patrol.NavMeshAgent.speed = 3.5f;

                            m_Behaviour = m_Zombies[z].transform.GetChild(0).gameObject;
                            m_Chase = m_Behaviour.GetComponent<BasicChaseTargetState>();
                            m_Chase.SetTarget(m_Player.GetComponent<IDamageableObject>());
                            m_Chase.Enter();
                        }
                    

                }
            }

        }

        public void DespawnZombie()
        {
            for (int z = 0; z < m_Zombies.Count; z++)
            {
                if (m_Zombies[z].activeInHierarchy)
                {
                    m_Zombies[z].SetActive(false);
                }
            }

        }


        public void SpawnZombies()
        {
                for (int z = 0; z < m_Zombies.Count; z++)
                {
                    if (!m_Zombies[z].activeInHierarchy)
                    {
                        m_Zombies[z].transform.position = m_Spawn.transform.position;
                        m_Zombies[z].SetActive(true);
                        m_Zombies[z].GetComponent<RegularDamageBehaviour>().ChangeHealth(100);

                        break;

                    }
                }
            }

        }
    }


