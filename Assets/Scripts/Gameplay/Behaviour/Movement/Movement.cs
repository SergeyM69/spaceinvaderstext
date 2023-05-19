using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Behaviour.Move
{
    public abstract class Movement : MonoBehaviour
    {
        public float MovementSpeed { get; set; }
        public Vector2 MovementDirection { get; set; }

        public bool IsActive { get; set; }
    }
}
