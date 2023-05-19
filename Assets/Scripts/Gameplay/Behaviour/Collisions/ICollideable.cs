using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Behaviour.Collisions
{
    public interface ICollideable
    {
        bool IsEnemy { get; }
        void Kill();
        CollisionType CollisionType { get; }
    }
}
