using SpaceInvaders.Behaviour.Move;
using SpaceInvaders.Events;
using SpaceInvaders.Gameplay.Entities;

using UnityEngine;

namespace SpaceInvaders.Behaviour.ShipControl
{
    public abstract class ShipControl : MonoBehaviour
    {
        protected bool isActive = false;


        [SerializeField] private Ship ship;
        [SerializeField] private Movement movement;


        public Ship Ship => ship;


        private void OnEnable()
        {
            EventsHub.onGamePaused += ShipControl_OnGamePaused;

            if (movement!= null)
            {
                movement.MovementSpeed = ship.MovementSpeed;
            }

            OnEnableImpl();
        }

        private void OnDisable()
        {
            EventsHub.onGamePaused -= ShipControl_OnGamePaused;

            OnDisableImpl();
        }

        private void Update()
        {
            if (isActive)
            {
                DoUpdate();
            }
        }

        protected virtual void OnEnableImpl()
        {

        }

        protected virtual void OnDisableImpl()
        {

        }

        protected virtual void DoUpdate()
        {

        }

        public void SetActive(bool isActive)
        {
            this.isActive= isActive;
        }


        private void ShipControl_OnGamePaused(bool isPaused)
        {
            isActive = !isPaused;
        }
    }
}
