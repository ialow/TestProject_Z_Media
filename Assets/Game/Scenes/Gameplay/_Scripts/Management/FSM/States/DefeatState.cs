using UnityEngine;
using Untils;

namespace Game
{
    public class DefeatState : IFSMState<StateGameplay>
    {
        private readonly DefeatView _view;

        public DefeatState(FSMGameplay fsm, DefeatView view)
        {
            _view = view;
            _view.Init(() => fsm.Exit(Scenes.Menu));
        }

        public StateGameplay State => StateGameplay.DefeatState;

        public void Enter()
        {
            _view.Enable();
        }

        public void Exit()
        {
            
        }
    }
}