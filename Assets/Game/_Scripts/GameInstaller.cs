using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameplayConfig
    {
        public int CountEntities = 20;
        public Func<EntitySpecification> PlayerSpecification;
        public Func<EntitySpecification> EnemySpecification;
    }

    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SpecificationsConfig _specificationsConfig;

        public override void InstallBindings()
        {
            Container.Bind<GameplayConfig>().AsSingle().NonLazy();
            Container.Bind<SpecificationsConfig>().FromInstance(_specificationsConfig).AsSingle();
        }
    }
}