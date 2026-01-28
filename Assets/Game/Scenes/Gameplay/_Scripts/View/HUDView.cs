using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private Button _timeScale;

        public void Init(Action changeTimeScale)
        {
            _timeScale.onClick.AddListener(() => changeTimeScale());
        }
    }
}
