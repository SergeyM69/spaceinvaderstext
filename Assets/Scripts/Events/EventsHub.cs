using SpaceInvaders.Gameplay.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Events
{
    public static class EventsHub
    {
        public static event Action onLevelStarted;
        public static event Action<Ship> onEnemyKilled;
        public static event Action<int> onScoreChanged;
        public static event Action onPlayerKilled;
        public static event Action onLevelRestartRequested;
        public static event Action onGameOver;
        public static event Action onTutorialCompleted;
        public static event Action<Vector2> onInputVectorChanged;
        public static event Action<bool> onFireButtonStateChanged;
        public static event Action<bool> onGamePaused;
        public static event Action<Collider2D, Collider2D> onCollisionEnter;

        public static void InvokeStartLevel()
        {
            onLevelStarted?.Invoke();
        }

        public static void InvokeGamePause(bool isPaused)
        {
            onGamePaused?.Invoke(isPaused);
        }

        public static void InvokeEnemyKilled(Ship enemyShip)
        {
            onEnemyKilled?.Invoke(enemyShip);
        }

        public static void InvokeScoreChanged(int newScore)
        {
            onScoreChanged?.Invoke(newScore);
        }
        public static void InvokePlayerKilled()
        {
            onPlayerKilled?.Invoke();
        }

        public static void InvokeLevelRestartRequested()
        {
            onLevelRestartRequested?.Invoke();
        }

        public static void InvokeOnGameOver()
        {
            onGameOver?.Invoke();
        }

        public static void InvokeTutorialCompleted()
        {
            onTutorialCompleted?.Invoke();
        }

        public static void InvokeInputVectorChanged(Vector2 newInputVector)
        {
            onInputVectorChanged?.Invoke(newInputVector);
        }

        public static void InvokeFireButtonStateChanged(bool isButtonPressed)
        {
            onFireButtonStateChanged?.Invoke(isButtonPressed);
        }

        public static void InvokeEnterCollision(Collider2D collider1, Collider2D collider2)
        {
            onCollisionEnter?.Invoke(collider1, collider2);
        }
    }
}
