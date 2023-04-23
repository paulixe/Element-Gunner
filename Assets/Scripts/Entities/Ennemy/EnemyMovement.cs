using PII.Dungeon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PII.Entities
{
    /// <summary>
    /// Use A* for moving the enemy
    /// </summary>
    [RequireComponent(typeof(Enemy))]
    public class EnemyMovement : MonoBehaviour
    {
        private Enemy enemy;
        private Stack<Vector3> currentPath;
        private float pathRebuildCooldown;
        private float startPathRebuildCooldown = 1;
        private Coroutine moveCoroutine;
        private WaitForFixedUpdate waitForFixedUpdate;
        Vector3 PlayerPosition => GameManager.Instance.Player.transform.position;
        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            waitForFixedUpdate = new WaitForFixedUpdate();
        }
        public void Update()
        {
            pathRebuildCooldown -= Time.deltaTime;

            if (pathRebuildCooldown < 0)
            {
                pathRebuildCooldown = startPathRebuildCooldown;
                if (moveCoroutine != null)
                {
                    enemy.IdleEvent.Call();
                    StopCoroutine(moveCoroutine);
                }

                Vector3 startPosition = transform.position + (Vector3)Settings.TileOffset;
                Vector3 endPosition = PlayerPosition + (Vector3)Settings.TileOffset;
                currentPath = DungeonManager.Instance.GetCurrentRoom().RoomAStar.GetPath(startPosition, endPosition);
                moveCoroutine = StartCoroutine(MoveEnemy());


            }
        }
        IEnumerator MoveEnemy()
        {
            while (currentPath.TryPop(out Vector3 nextPosition))
            {
                while (Vector3.Distance(transform.position, nextPosition) > 0.2f)
                {
                    enemy.MovementByPositionEvent.Call(nextPosition, enemy.EnemyDetails.MoveSpeed);
                    yield return waitForFixedUpdate;
                }
                yield return waitForFixedUpdate;
            }
            enemy.IdleEvent.Call();
        }
    }
}