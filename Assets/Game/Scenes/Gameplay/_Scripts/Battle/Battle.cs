using System;

namespace Game
{
    public enum BattleResult
    {
        InProgress,
        PlayerVictory,
        EnemyVictory,
        Draw
    }

    public class Battle
    {
        private Formation _playerFormation;
        private Formation _enemyFormation;

        public BattleResult Result { get; private set; } = BattleResult.InProgress;

        public void Init(Formation player, Formation enemy)
        {
            _playerFormation = player;
            _enemyFormation = enemy;
        }

        public void Update(float timeScale)
        {
            if (Result != BattleResult.InProgress) return;

            _playerFormation.Update(_enemyFormation, timeScale);
            _enemyFormation.Update(_playerFormation, timeScale);

            Result = CalculateResult();
        }

        private BattleResult CalculateResult()
        {
            var playerAlive = _playerFormation.IsAnyAlive();
            var enemyAlive = _enemyFormation.IsAnyAlive();

            if (!playerAlive && !enemyAlive) return BattleResult.Draw;
            if (!enemyAlive) return BattleResult.PlayerVictory;
            if (!playerAlive) return BattleResult.EnemyVictory;

            return BattleResult.InProgress;
        }

    }
}
