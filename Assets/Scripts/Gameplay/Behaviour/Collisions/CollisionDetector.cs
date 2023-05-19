using SpaceInvaders.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Behaviour.Collisions
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private Collider2D collider;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            EventsHub.InvokeEnterCollision(collider, collision.collider);
        }
    }
}
