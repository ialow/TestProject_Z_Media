using Untils;

namespace Game
{
    public class GameLoopState : ISuspendFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fsm;
        private readonly Battle _battle;
        private readonly Gameplay _gameplay;
        private readonly HUDView _view;

        private readonly float[] _speedSteps = { 1.0f, 0.2f };
        private int _currentSpeedIndex = 0;
        private float _currentTimeScale;

        public GameLoopState(FSMGameplay fsm, HUDView view, Battle battle, Gameplay gameplay)
        {
            _fsm = fsm;
            _view = view;
            _battle = battle;
            _gameplay = gameplay;
            _currentTimeScale = _speedSteps[0];

            _view.Init(ToggleSpeed);
        }

        public StateGameplay State => StateGameplay.GameLoopState;

        public void ToggleSpeed()
        {
            _currentSpeedIndex = (_currentSpeedIndex + 1) % _speedSteps.Length;
            _currentTimeScale = _speedSteps[_currentSpeedIndex];
        }

        public void Enter()
        {
            _gameplay.SetLoop(() =>
            {
                _battle.Update(_currentTimeScale);

                if (_battle.Result != BattleResult.InProgress)
                {
                    _gameplay.SetLoop(null);

                    if (_battle.Result == BattleResult.PlayerVictory)
                    {
                        _fsm.EnterIn(StateGameplay.VictoryState);
                    }
                    else
                    {
                        _fsm.EnterIn(StateGameplay.DefeatState);
                    }
                }
            });
        }

        public void Exit()
        {
            _currentTimeScale = 1.0f;
            _currentSpeedIndex = 0;
        }

        public void Resume()
        {
            throw new System.NotImplementedException();
        }

        public void Suspend()
        {
            throw new System.NotImplementedException();
        }
    }
}