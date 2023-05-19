using SpaceInvaders.Behaviour.ShipControl;
using SpaceInvaders.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Entities
{
    public class PlayerShipControl : ShipControl
    {
        private bool isShooting = false;

        protected override void OnEnableImpl()
        {
            base.OnEnableImpl();
            EventsHub.onInputVectorChanged += PlayerShipControl_OnInputVectorChanged;
            EventsHub.onFireButtonStateChanged += PlayerShipControl_OnFireButtonStateChanged;
        }

        protected override void OnDisableImpl()
        {
            base.OnDisableImpl();

            EventsHub.onInputVectorChanged -= PlayerShipControl_OnInputVectorChanged;
            EventsHub.onFireButtonStateChanged -= PlayerShipControl_OnFireButtonStateChanged;
        }

        private void PlayerShipControl_OnInputVectorChanged(Vector2 directionVector)
        {
            if (isActive)
            {
                Ship.Movement.MovementDirection = directionVector;
            }
        }

        private void PlayerShipControl_OnFireButtonStateChanged(bool isActive)
        {
            isShooting = isActive;
        }

        protected override void DoUpdate()
        {
            base.DoUpdate();

            if (isShooting)
            {
                Ship.Weapon.Fire(Vector2.up);
            }
        }
    }
}
