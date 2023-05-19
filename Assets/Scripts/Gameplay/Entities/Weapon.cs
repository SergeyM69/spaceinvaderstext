using SpaceInvaders.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Entities
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float timeBetweenShots;

        private float cooldown;

        public void Fire(Vector2 direction)
        {
            if (Time.time > cooldown)
            {
                var bullet = Pools.Get(bulletPrefab.gameObject).GetComponent<Bullet>();
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.name = "TestBullet";
                bullet.gameObject.SetActive(true);
                bullet.Fire(direction);
                cooldown = Time.time + timeBetweenShots;
            }
        }
    }
}
