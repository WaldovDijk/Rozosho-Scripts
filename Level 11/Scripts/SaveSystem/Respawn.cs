using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level11
{
    public class Respawn : MonoBehaviour
    {
        [SerializeField]
        private IDamageableObject m_DamageableObject;
        public IDamageableObject DamageableObject
        {
            get { return m_DamageableObject; }
        }

        [SerializeField] private GameObject m_Player;

        private ProgressManager m_Progress;

        [SerializeField] private List<GameObject> m_Objects;

        [SerializeField] private List<GameObject> m_SpawnPositions;

        public int m_SpawnPoint;

        private Save m_Save = new Save();

        public bool m_Reload = false;

        Vector3 m_Pos;


        private void Start()
        {
            m_Progress = GetComponent<ProgressManager>();
            if (m_Reload)
                Load();
            LoadData();
        }
        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.O))
                Load();

            if (Input.GetKeyDown(KeyCode.L))
                Save();

            if (Input.GetKeyDown(KeyCode.K))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
               
            if (m_DamageableObject.Health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }
        public void Save()
        {
            m_Reload = true;

            SaveData();


        }
        public void Load()
        {


            LoadData();

            if (m_SpawnPoint == 0)
            {
                m_Player.transform.position = m_SpawnPositions[0].transform.position;
                m_Progress.Progress = 0;
            }
            if (m_SpawnPoint == 1)
            {
                m_Player.transform.position = m_SpawnPositions[1].transform.position;
                m_Progress.Progress = 1;
            }
            if (m_SpawnPoint == 2)
            {
                m_Player.transform.position = m_SpawnPositions[2].transform.position;
                m_Progress.Progress = 4;
                m_Progress.m_Timer = 0f;
            }
        }


        public void SetSpawnPoint(int SpawnInt)
        {
            if (m_SpawnPoint < SpawnInt)
            {
                m_SpawnPoint = SpawnInt;
                SaveData();
            }
            
        }


        public void SaveData()
        {
            m_Save.m_SpawnPoint = m_SpawnPoint;
            m_Save.m_Reload = m_Reload;


            string json = JsonUtility.ToJson(m_Save);
            File.WriteAllText(Application.persistentDataPath + "/SaveData.json", json);

            Debug.Log("Saved");

        }
        public void LoadData()
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
            m_Save = JsonUtility.FromJson<Save>(json);
            m_SpawnPoint = m_Save.m_SpawnPoint;
            m_Reload = m_Save.m_Reload;
        }

    }
}




