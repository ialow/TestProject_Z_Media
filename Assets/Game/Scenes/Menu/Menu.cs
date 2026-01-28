using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class Menu : MonoBehaviour
    {
        [Inject] private GameplayConfig _gameplayConfig;
        [Inject] private SpecificationsConfig _allSpecificationsConfig;

        [SerializeField] private Button _smallFormation;
        [SerializeField] private Button _largeFormation;
        [SerializeField] private Button _defaultGame;
        [SerializeField] private Button _randomGame;

        private void Awake()
        {
            _smallFormation.onClick.AddListener(() => _gameplayConfig.CountEntities = 10);
            _largeFormation.onClick.AddListener(() => _gameplayConfig.CountEntities = 20);
            
            _defaultGame.onClick.AddListener(() => { 
                DefaultSetup();
                SceneManager.LoadScene(Scenes.Gameplay);
            });

            _randomGame.onClick.AddListener(() => { 
                RandomSetup();
                SceneManager.LoadScene(Scenes.Gameplay);
            });
        }

        private void DefaultSetup()
        {
            _gameplayConfig.PlayerSpecification = () => _allSpecificationsConfig.GetSpecification(1);
            _gameplayConfig.EnemySpecification = () => _allSpecificationsConfig.GetSpecification(0);
        }

        private void RandomSetup()
        {
            _gameplayConfig.PlayerSpecification = () => _allSpecificationsConfig.GetRandomSpecification();
            _gameplayConfig.EnemySpecification = () => _allSpecificationsConfig.GetRandomSpecification();
        }
    }
}
