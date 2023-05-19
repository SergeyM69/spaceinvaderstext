using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Pooling
{
    public static class Pools
    {
        private static Dictionary<GameObject, List<GameObject>> pools = new Dictionary<GameObject, List<GameObject>>();
        public static void AddPool(GameObject prefab, int initialCount)
        {
            if (pools.ContainsKey(prefab))
            {
                throw new System.Exception($"Can't add a pool for prefab {prefab.name}. The pool already exists");
            }

            pools[prefab] = new List<GameObject>();

            for (int i = 0; i < initialCount; i++) 
            {
                AddNewInstanceToPool(prefab);
            }
        }

        public static void Put(GameObject instance)
        {
            var component = instance.GetComponent<PoolObject>();
            if (component == null)
            {
                GameObject.Destroy(instance);
                return;
            }

            if (!pools.ContainsKey(component.Prefab)) 
            {
                GameObject.Destroy(instance);
                return;
            };

            pools[component.Prefab].Add(instance);
            instance.gameObject.SetActive(false);
        }

        public static GameObject Get(GameObject prefab)
        {
            if (!pools.ContainsKey(prefab))
            {
                var newInstance = GameObject.Instantiate(prefab);
                return newInstance;
            }

            var list = pools[prefab];
            GameObject instance;
            if (list.Count > 0)
            {
                instance = pools[prefab][0];
                pools[prefab].RemoveAt(0);
            }
            else
            {
                AddNewInstanceToPool(prefab);
                instance = Get(prefab);
            }

            return instance;
        }

        private static GameObject AddNewInstanceToPool(GameObject prefab)
        {
            var instance = Instantiate(prefab);
            pools[prefab].Add(instance);
            instance.SetActive(false);

            return instance;
        }

        private static GameObject Instantiate(GameObject prefab)
        {
            var instance = GameObject.Instantiate(prefab);
            var component = instance.AddComponent<PoolObject>();
            component.Prefab = prefab;

            return instance;
        }
    }
}
