using SpaceInvaders.Events;
using SpaceInvaders.Gameplay.Entities;
using SpaceInvaders.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Gameplay
{
    public class Game : MonoBehaviour
    {
        private const int ScorePerShip = 10;

        [SerializeField] private Transform playerStartingPosition;
        [SerializeField] private Ship playerShipPrefab;
        [SerializeField] private PoolInfo[] poolInfos;
        [SerializeField] private EnemySpawn[] enemySpawns;
        [SerializeField] private WeaponBonus weaponBonusPrefab;

        [SerializeField] private Vector2Int minMaxEnemiesToKillToGetAWeaponBonus;

        private int score = 0;
        private int remainingEnemiesToKillToGetABonus;

        private int Score
        {
            get { return score; }
            set
            { 
                score = value;

                EventsHub.InvokeScoreChanged(score);
            }
        }


        private readonly List<Ship> aliveEnemyShips = new List<Ship>();
        private Ship playerShip;

        private void Awake()
        {
            InitPools();
            EventsSubsribe();
            StartLevel();
        }

        private void OnDestroy()
        {
            EventsUnsubscribe();
        }

        private void InitPools()
        {
            foreach(var poolInfo in poolInfos)
            {
                Pools.AddPool(poolInfo.Prefab, poolInfo.InitialCount);
            }
        }

        private void EventsSubsribe()
        {
            EventsHub.onTutorialCompleted += Game_OnTutorialCompleted;
            EventsHub.onPlayerKilled += Game_OnPlayerKilled;
            EventsHub.onLevelRestartRequested += Game_OnLevelRestartRequested;
            EventsHub.onEnemyKilled += Game_OnEnemyKilled;
        }

        private void EventsUnsubscribe()
        {
            EventsHub.onPlayerKilled -= Game_OnPlayerKilled;
            EventsHub.onLevelRestartRequested -= Game_OnLevelRestartRequested;
            EventsHub.onEnemyKilled -= Game_OnEnemyKilled;
        }

        private void Game_OnTutorialCompleted()
        {
            EventsHub.onTutorialCompleted -= Game_OnTutorialCompleted;
            EventsHub.InvokeGamePause(false);
        }

        private void Game_OnLevelRestartRequested()
        {
            StartLevel();
            EventsHub.InvokeGamePause(false);
        }

        private void Game_OnEnemyKilled(Ship enemyShip)
        {
            aliveEnemyShips.Remove(enemyShip);
            Score += ScorePerShip;

            remainingEnemiesToKillToGetABonus--;
            if (remainingEnemiesToKillToGetABonus <= 0)
            {
                GenerateRandomEnemiesCountToGetBonus();

                var bonus = Pools.Get(weaponBonusPrefab.gameObject);
                bonus.transform.position = enemyShip.transform.position;
                bonus.gameObject.SetActive(true);
            }

            if (aliveEnemyShips.Count == 0)
            {
                StartLevel();
            }
        }

        private void GenerateRandomEnemiesCountToGetBonus()
        {
            remainingEnemiesToKillToGetABonus = Random.Range(minMaxEnemiesToKillToGetAWeaponBonus.x,
                minMaxEnemiesToKillToGetAWeaponBonus.y);
        }

        private void StartLevel()
        {
            Score = 0;

            GenerateRandomEnemiesCountToGetBonus();

            SpawnPlayer();
            SpawnEnemies();
        }

        private void Game_OnPlayerKilled()
        {
            EventsHub.InvokeGamePause(true);
            EventsHub.InvokeOnGameOver();
        }

        private void SpawnPlayer()
        {
            if (playerShip != null)
            {
                Pools.Put(playerShip.gameObject);
            }

            playerShip = Pools.Get(playerShipPrefab.gameObject).GetComponent<Ship>();
            playerShip.transform.position = playerStartingPosition.position;
            playerShip.gameObject.SetActive(true);
        }

        private void SpawnEnemies()
        {
            foreach(var ship in aliveEnemyShips)
            {
                Pools.Put(ship.gameObject);
            }

            aliveEnemyShips.Clear();

            foreach(var enemySpawn in enemySpawns)
            {
                var newEnemyShip = SpawnShip(enemySpawn.Prefab, enemySpawn.transform);
                aliveEnemyShips.Add(newEnemyShip);
            }
        }

        private Ship SpawnShip(Ship prefab, Transform position)
        {
            var instance = GameObject.Instantiate(prefab,position.position, Quaternion.identity);
            return instance;
        }
    }
}
