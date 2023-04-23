using PII.Dungeon;
using System.Collections.Generic;
using UnityEngine;
using Utilities.ObjectPooling;

namespace PII.Entities
{
    /// <summary>
    /// Spawn enemies when you enter a room not visited
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        public int EnemyCount { get; private set; } = 0;
        List<EntityDetailsSO> enemyDetails => GameResources.Instance.EnemyDetails;
        private void OnEnable()
        {
            StaticEventHandler.OnRoomChange += OnRoomChange;
        }
        private void OnDisable()
        {
            StaticEventHandler.OnRoomChange -= OnRoomChange;
        }
        private void OnRoomChange(RoomChangedEventArgs roomChangedEventArgs)
        {
            InstantiatedRoom room = roomChangedEventArgs.CurrentRoom;
            if (room.Room.IsCorridor || room.Room.RoomType == RoomType.Entrance)
                return;
            if (room.HasBeenVisited)
                return;
            room.LockDoors();

            SpawnEnemies(room);
        }
        private void SpawnEnemies(InstantiatedRoom room)
        {
            EnemyCount = 0;
            foreach (Vector2Int spawnPos in room.Room.RoomTemplate.spawnPositionArray)
            {
                SpawnEnemyAt(spawnPos, room);
                EnemyCount++;
            }
            StaticEventHandler.ChangeGameState(GameState.engagingEnemies);
        }

        private void SpawnEnemyAt(Vector2Int spawnPos, InstantiatedRoom room)
        {
            EntityDetailsSO randomEnemyDetails = enemyDetails[Random.Range(0, enemyDetails.Count)];
            Vector3 worldPos = room.Grid.CellToWorld((Vector3Int)spawnPos);
            Enemy enemy = PoolManager.Instance.GetComponent(randomEnemyDetails.Prefab, worldPos, Quaternion.identity) as Enemy;
            enemy.Initialize(randomEnemyDetails);
            enemy.DestroyedEvent.OnDestroy += EnemyDestroyedEvent_OnDestroy;
            enemy.gameObject.SetActive(true);
        }
        private void EnemyDestroyedEvent_OnDestroy(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
        {
            destroyedEvent.OnDestroy -= EnemyDestroyedEvent_OnDestroy;
            EnemyCount--;
            if (EnemyCount <= 0)
                StaticEventHandler.CallRoomCleared();
        }

    }
}