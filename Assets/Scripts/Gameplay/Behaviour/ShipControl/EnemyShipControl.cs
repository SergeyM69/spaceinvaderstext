using SpaceInvaders.Behaviour.ShipControl;
using SpaceInvaders.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Entities
{
    public class EnemyShipControl : ShipControl
    {
        [SerializeField] private float movementTime = 3.0f;
        [SerializeField] private float timeBetweenShots = 3.0f;
        [SerializeField] private float initialDelay = 1.0f;
        [SerializeField] private bool isRightDirection = true;

        private float timeRemainingUntilChangeDirection;
        private float timeRemainingUntilTheNextShot;
        private bool isCurrentMovementDirectionRight = true;

        protected override void OnEnableImpl()
        {
            base.OnEnableImpl();
            timeRemainingUntilChangeDirection = 0.5f * movementTime;
            isCurrentMovementDirectionRight = isRightDirection;
            timeRemainingUntilTheNextShot = timeBetweenShots + Random.Range(0.0f, initialDelay);
        }

        protected override void OnDisableImpl()
        {
            base.OnDisableImpl();

            timeRemainingUntilTheNextShot = timeBetweenShots;
            timeRemainingUntilChangeDirection = timeBetweenShots;
        }

        protected override void DoUpdate()
        {
            base.DoUpdate();

            timeRemainingUntilTheNextShot -= Time.deltaTime;
            if (timeRemainingUntilTheNextShot <= 0.0f)
            {
                Ship.Weapon.Fire(Vector2.down);
                timeRemainingUntilTheNextShot = timeBetweenShots;
            }

            timeRemainingUntilChangeDirection -= Time.deltaTime;
            if (timeRemainingUntilChangeDirection <= 0.0f)
            {
                isCurrentMovementDirectionRight = !isCurrentMovementDirectionRight;
                timeRemainingUntilChangeDirection = movementTime;
            }

            Ship.Movement.MovementDirection = isCurrentMovementDirectionRight ? Vector2.right : Vector2.left;
        }
    }
}