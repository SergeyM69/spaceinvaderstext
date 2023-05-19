using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Pooling
{
    [Serializable]
    public class PoolInfo
    {
        public GameObject Prefab;
        public int InitialCount;
    }
}
