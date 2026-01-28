using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DefeatView : MonoBehaviour
    {
        [SerializeField] private Button _nextState;

        public void Init(Action nextStateButton)
        {
            _nextState.onClick.AddListener(() => nextStateButton());
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }
    }
}
