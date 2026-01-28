using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Gameplay : MonoBehaviour
    {
        [Inject] private FSMGameplay _fSMGameplay;

        private void Start()
        {
            _fSMGameplay.EnterIn(StateGameplay.InitState);
        }

        private Action _loop;

        public void SetLoop(Action loop) => _loop = loop;

        public void Update()
        {
            _loop?.Invoke();
        }
    }
}