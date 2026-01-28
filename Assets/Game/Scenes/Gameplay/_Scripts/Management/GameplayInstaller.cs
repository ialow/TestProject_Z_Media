using UnityEngine;
using Zenject;

namespace Game
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Entity _baseEntity;
        [SerializeField] private EntityData _baseEntityData;

        public override void InstallBindings()
        {            
            Container.Bind<Entity>().FromInstance(_baseEntity).AsSingle();
            Container.Bind<EntityData>().FromInstance(_baseEntityData).AsSingle();

            Container.Bind<World>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Gameplay>().FromComponentInHierarchy().AsSingle();
            Container.Bind<DefeatView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<VictoryView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<HUDView>().FromComponentInHierarchy().AsSingle();

            Container.Bind<EntityFactory>().AsSingle();
            Container.Bind<Battle>().AsSingle();

            Container.BindInterfacesAndSelfTo<InitState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoopState>().AsSingle();
            Container.BindInterfacesAndSelfTo<VictoryState>().AsSingle();
            Container.BindInterfacesAndSelfTo<DefeatState>().AsSingle();

            Container.Bind<FSMGameplay>().AsSingle();
        }
    }
}