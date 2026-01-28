using Untils;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Game
{
    public class InitState : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fsm;
        private readonly EntityFactory _entityFactory;
        private readonly Battle _battle;
        private readonly World _world;
        private readonly GameplayConfig _gameplayConfig;

        public InitState(FSMGameplay fsm, Battle battle, EntityFactory entityFactory, World world, GameplayConfig gameplayConfig)
        {
            _fsm = fsm;
            _entityFactory = entityFactory;
            _world = world;
            _battle = battle;

            _gameplayConfig = gameplayConfig;
        }

        public StateGameplay State => StateGameplay.InitState;

        public void Enter()
        {
            var playerFormation = CreateFormation(
                new DefauleFormationBuilder(_gameplayConfig.CountEntities, 10, 1, _world.PlayerTeamOrign.position),
                _world.PlayerTeamOrign, _gameplayConfig.PlayerSpecification);

            var enemyFormation = CreateFormation(
                new DefauleFormationBuilder(_gameplayConfig.CountEntities, 10, 1, _world.EnemyTeamOrign.position),
                _world.EnemyTeamOrign, _gameplayConfig.EnemySpecification);

            _battle.Init(playerFormation, enemyFormation);
            _fsm.EnterIn(StateGameplay.GameLoopState);
        }

        public void Exit()
        {
            
        }

        private Formation CreateFormation(IFormationBuilder formationBuilder, Transform container, Func<EntitySpecification> getSpecification)
        {
            var gridRows = formationBuilder.CreateGrid();
            var formationRows = new List<List<FormationEntity>>();

            foreach (var rowPositions in gridRows)
            {
                var currentRowEntities = new List<FormationEntity>();

                foreach (var position in rowPositions)
                {
                    var entity = _entityFactory.Create(position, container, getSpecification());
                    var formationEntity = new FormationEntity(entity);

                    entity.Init(formationEntity);
                    currentRowEntities.Add(formationEntity);
                }

                formationRows.Add(currentRowEntities);
            }

            return new Formation(formationRows, new DefaultFormationBehaviour());
        }
    }
}