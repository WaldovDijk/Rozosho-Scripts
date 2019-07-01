using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level11
{
    [Serializable]
    public class PoolingObject
    {
        [SerializeField]
        private GameObject m_ObjectType;
        public GameObject ObjectType
        {
            get { return m_ObjectType; }
        }

        [SerializeField]
        private int m_Amount;
        public int Amount
        {
            get { return m_Amount; }
        }
    }

}
