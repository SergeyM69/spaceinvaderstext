using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceInvaders.Pooling;
using SpaceInvaders.Behaviour.Move;
using SpaceInvaders.Events;
using SpaceInvaders.Behaviour.Collisions;

namespace SpaceInvaders.Gameplay.Entities
{
    public class Ship : MonoBehaviour, ICollideable
    {
        [SerializeField] private Transform root;
        [SerializeField] private float movementSpeed;
        [SerializeField] private bool isEnemy;
        [SerializeField] private Movement movement;
        [SerializeField] private Transform weaponMount;
        [SerializeField] private Weapon initialWeaponPrefab;

        private Weapon weapon;

        public float MovementSpeed => movementSpeed;
        public Movement Movement => movement;
        public Weapon Weapon => weapon;
        public bool IsEnemy => isEnemy;

        public CollisionType CollisionType => CollisionType.Damage;

        private void OnEnable()
        {
            EventsHub.onCollisionEnter += Ship_OnCollisionEnter;

            DestroyWeapon();
            CreateWeapon(initialWeaponPrefab.gameObject);

            weapon.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            EventsHub.onCollisionEnter -= Ship_OnCollisionEnter;
        }

        private void DestroyWeapon()
        {
            if (weapon != null)
            {
                weapon.transform.SetParent(null);
                Pools.Put(weapon.gameObject);
            }
        }

        private void CreateWeapon(GameObject prefab)
        {
            weapon = Pools.Get(prefab)
                .GetComponent<Weapon>();
            weapon.transform.SetParent(weaponMount, false);
        }

        public void Kill()
        {
            gameObject.SetActive(false);

            Pools.Put(gameObject);

            if (isEnemy)
            {
                EventsHub.InvokeEnemyKilled(this);
            }
            else
            {
                EventsHub.InvokePlayerKilled();
            }
        }

        private void Ship_OnCollisionEnter(Collider2D collider1, Collider2D collider2)
        {
            var collideable1 = collider1.gameObject.GetComponent<ICollideable>();
            var collideable2 = collider2.gameObject.GetComponent<ICollideable>();

            var isItMe = collideable1 == this || collideable2 == this;
            if (isItMe)
            {
                var incomingObject = collideable1 == this ? collideable2 : collideable1;
                switch (incomingObject.CollisionType)
                {
                    case CollisionType.None:
                        break;
                    case CollisionType.Damage:
                        var isEnemy = collideable1.IsEnemy != collideable2.IsEnemy;
                        if (isEnemy)
                        {
                            Kill();
                        }
                        break;
                    case CollisionType.Bonus:
                        if (!this.isEnemy)
                        {
                            incomingObject.Kill();

                            var bonus = incomingObject as WeaponBonus;
                            DestroyWeapon();
                            CreateWeapon(bonus.Prefab.gameObject);
                        }
                        break;
                }
            }
        }
    }
}
