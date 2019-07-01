using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;

namespace Level02
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField]
        private List<PoolingObject> m_PoolObjects;

        [SerializeField]private int m_Progress = 0;
        [SerializeField] private VisualEffect m_CrashSmoke;

        public int Progress
        {
            get { return m_Progress; }
            set { m_Progress = value; }
        }

        [SerializeField] private GameObject m_Door;

        [SerializeField]
        private List<GameObject> m_Zombies;
        public List<GameObject> Zombie
        {
            get { return m_Zombies; }
        }

        [SerializeField] private List<GameObject> m_Enemy;
        [SerializeField] private Text m_Text1, m_Text2;
        [SerializeField] private GameObject m_BeforeCrash, m_AfterCrash, m_MainUI, m_EndUI, m_Visuals;
        [SerializeField] private Text m_Text3, m_Text4;

        GameObject m_PoolName;

        void Awake()
        {

            m_CrashSmoke.SetFloat("Size", 0);
            m_CrashSmoke.SetFloat("SpawnRate", 0);

            foreach (GameObject obj in m_Enemy)
            {
                obj.SetActive(false);
            }
        }
        

        void Start()
        {
            for (int p = 0; p < m_PoolObjects.Count; p++)
            {
                m_PoolName = new GameObject("Pool" + " " + m_PoolObjects[p].ObjectType.name);
                m_PoolName.transform.parent = gameObject.transform;
                for (int i = 0; i < m_PoolObjects[p].Amount; i++)
                {
                    if (m_PoolObjects[p].ObjectType.name == "Zombie")
                    {
                        GameObject obj = (GameObject) Instantiate(m_PoolObjects[p].ObjectType);
                        obj.transform.parent = m_PoolName.transform;
                        obj.SetActive(false);
                        m_Zombies.Add(obj);
                    }
                }
            }
        }

        void Update()
        {
            if (m_Progress > 0)
            {
                m_Door.SetActive(false);
            }
            UiProgress();
        }

        void UiProgress()
        {
            switch (m_Progress)
            {
                case 0:
                    
                    break;
                case 1:
                    m_Text1.text = "1: M̶a̶k̶e̶ ̶t̶h̶e̶ ̶s̶h̶i̶p̶ ̶a̶n̶d̶ ̶l̶a̶u̶n̶c̶h̶p̶a̶d̶ ̶r̶e̶a̶d̶y̶ ";
                    m_Text2.text = "2: Go to the ship!";
                    break;
                case 2:
                    m_BeforeCrash.SetActive(false);
                    m_AfterCrash.SetActive(true);
                    break;
                case 3:
                    m_Text3.text = "1: U̶s̶e̶ ̶t̶h̶e̶ ̶c̶r̶a̶n̶e̶ ̶t̶o̶ ̶o̶p̶e̶n̶ ̶t̶h̶e̶ ̶b̶i̶g̶ ̶d̶o̶o̶r̶ ";
                    break;
                case 4:
                    m_Text4.text = "2: C̶o̶l̶l̶e̶c̶t̶ ̶t̶h̶e̶ ̶f̶u̶e̶l̶ ̶c̶e̶l̶l̶s̶ ̶t̶o̶ ";
                    break;
                case 5:

                    m_BeforeCrash.SetActive(false);
                    m_AfterCrash.SetActive(false);
                    m_MainUI.SetActive(false);
                    m_Visuals.SetActive(false);
                    m_EndUI.SetActive(true);
                    break;
            }
        }

        public void Crash()
        {
            m_CrashSmoke.SetFloat("Size", 20);
            m_CrashSmoke.SetFloat("SpawnRate", 50);
            foreach (GameObject obj in m_Enemy)
            {
                obj.SetActive(true);
            }
        }


    }
}
