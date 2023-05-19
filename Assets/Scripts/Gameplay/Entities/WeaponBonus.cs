using SpaceInvaders.Behaviour.Collisions;
using SpaceInvaders.Behaviour.Move;
using SpaceInvaders.Events;
using SpaceInvaders.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Entities
{
    public class WeaponBonus : MonoBehaviour, ICollideable
    {
        [SerializeField] private Movement movement;
        [SerializeField] private float speed;
        [SerializeField] private bool isEnemy;
        [SerializeField] private float lifetime = 3.0f;
        [SerializeField] private Weapon prefab;

        private float deathTime;
        private bool isDestroyed;

        public bool IsEnemy => isEnemy;
        public Weapon Prefab => prefab;
        public CollisionType CollisionType => CollisionType.Bonus;

        public void OnEnable()
        {
            isDestroyed = false;
            deathTime = Time.time + lifetime;
            movement.MovementSpeed = speed;
            movement.MovementDirection = Vector2.down;
            movement.IsActive = true;
        }


        private void Update()
        {
            if (isDestroyed)
            {
                return;
            }

            if (Time.time > deathTime)
            {
                Kill();
            }
        }

        public void Kill()
        {
            isDestroyed = true;
            gameObject.SetActive(false);
            Pools.Put(gameObject);
        }
    }
}
