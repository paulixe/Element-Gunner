using PII.Dungeon;
using PII.Entities;
using UnityEngine;
using Utilities;
namespace PII
{
    /// <summary>
    /// Manage the game state and reference some components
    /// </summary>
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        [HideInInspector] public Player Player;
        [HideInInspector] public GameState CurrentGameState;
        [HideInInspector] public int CurrentLevel = 0;
        public GameResources Resources => GameResources.Instance;
        private InstantiatedRoom CurrentRoom => DungeonManager.Instance.GetCurrentRoom();
        private void Update()
        {
            //Test LevelBuilding
            //if (Input.GetKeyDown(KeyCode.A))
            //    PlayLevel(CurrentLevel);
        }
        private void OnEnable()
        {
            StaticEventHandler.OnGameStateChange += OnGameStateChange;
        }
        private void OnDisable()
        {
            StaticEventHandler.OnGameStateChange -= OnGameStateChange;
        }


        private void OnGameStateChange(GameStateChangeEventArgs gameStateChangeEventArgs)
        {
            CurrentGameState = gameStateChangeEventArgs.State;
        }

        private void Start()
        {
            PlayLevel(CurrentLevel);
            InstantiatePlayer();
        }
        private void InstantiatePlayer()
        {

            GameObject PlayerGameObject = Instantiate(Resources.PlayerDetails.Prefab, new Vector3(0, 0, 0), Quaternion.identity);
            PlayerGameObject.transform.position =
                CurrentRoom.GetClosestSpawningPositionTo(CurrentRoom.GetCenterPosition());
            Player = PlayerGameObject.GetComponent<Player>();
        }
        public void PlayLevel(int level)
        {
            DungeonManager.Instance.ChangeDungeon(Resources.RoomTemplates, Resources.GraphTemplates);
        }
    }
}