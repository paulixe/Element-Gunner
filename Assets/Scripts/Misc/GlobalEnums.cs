namespace PII
{
    public enum Element
    {
        Fire, Water, Earth, Wind, Neutral
    }
    public enum GameState
    {
        gameStarted,
        playingLevel,
        engagingEnemies,
        clearedRoom,
        bossStage,
        engagingBoss,
        levelCompleted,
        gameWon,
        gameLost,
        gamePaused,
        dungeonOverviewMap,
        restartGame
    }
}