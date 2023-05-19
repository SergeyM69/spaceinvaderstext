using SpaceInvaders.Behaviour.Collisions;
using SpaceInvaders.Behaviour.Move;
using SpaceInvaders.Events;
using SpaceInvaders.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Entities
{
    public class Bullet : MonoBehaviour, ICollideable
    {
        [SerializeField] private Movement movement;
        [SerializeField] private float speed;
        [SerializeField] private bool isEnemy;
        [SerializeField] private float lifetime = 3.0f;

        private float deathTime;
        private bool isDeatroyed;

        public bool IsEnemy => isEnemy;

        public CollisionType CollisionType => CollisionType.Damage;

        public void OnEnable()
        {
            isDeatroyed = false;

            EventsHub.onCollisionEnter += Bullet_OnCollisionEnter;
            EventsHub.onLevelStarted += Bullet_OnLevelStarted;

            deathTime = Time.time + lifetime;
        }

        public void OnDisable()
        {
            EventsHub.onCollisionEnter -= Bullet_OnCollisionEnter;
        }

        public void Fire(Vector2 direction)
        {
            if (movement != null)
            {
                movement.MovementDirection = direction;
                movement.MovementSpeed = speed;
                movement.IsActive = true;
            }
        }

        private void Update()
        {
            if (isDeatroyed)
            {
                return;
            }

            if (Time.time > deathTime)
            {
                Kill();
            }
        }
        private void Bullet_OnLevelStarted() => Kill();

        private void Bullet_OnCollisionEnter(Collider2D collider1, Collider2D collider2)
        {
            var collideable1 = collider1.gameObject.GetComponent<ICollideable>();
            var collideable2 = collider2.gameObject.GetComponent<ICollideable>();

            var isItMe = collideable1 == this || collideable2 == this;
            var isItShip = collideable1 is Ship || collideable2 is Ship;
            var isItEnemy = collideable1.IsEnemy != collideable2.IsEnemy;

            if (isItMe && isItShip && isItEnemy)
            {
                Kill();
            }
        }

        public void Kill()
        {
            isDeatroyed = true;
            gameObject.SetActive(false);
            Pools.Put(gameObject);
        }
    }
}
