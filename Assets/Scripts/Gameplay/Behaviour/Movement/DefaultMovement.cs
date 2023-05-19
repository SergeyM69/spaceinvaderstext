using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Behaviour.Move
{
    public class DefaultMovement : Movement
    {
        private void FixedUpdate()
        {
            var offset = Time.fixedDeltaTime * MovementSpeed * (Vector3)MovementDirection;
            var newPosition = gameObject.transform.position + offset;

            gameObject.transform.position = newPosition;
        }
    }
}
