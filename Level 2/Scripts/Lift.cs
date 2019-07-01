using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level02
{
    public class Lift : MonoBehaviour
    {
        [SerializeField] private Transform m_Down, m_Up;
        [SerializeField] private float m_Fraction = 0, m_Speed = 0.5F;
         public bool m_Top;

        void Start()
        {
            m_Top = false;
        }


        void Update()
        {

             MoveLift(m_Down, m_Up);
        }
        

      

        void MoveLift(Transform _Start,Transform _Stop)
        {
            if (m_Fraction < 1 && !m_Top)
            {
                m_Fraction += m_Speed * Time.deltaTime ;
                transform.position = Vector3.Lerp(_Start.position, _Stop.position, m_Fraction);
            }

            if (m_Fraction > 0 && m_Top)
            {
                m_Fraction -= Time.deltaTime * m_Speed;
                transform.position = Vector3.Lerp(_Start.position, _Stop.position, m_Fraction);
            }
        }

        
    }
}

