# Test Prototype (Z Media Task)
Данный проект разработан в качестве тестового задания для компании Z Media. Основной фокус направлен на создание чистой и масштабируемой архитектуры, пригодной для поддержки и развития средних игровых проектов.

---

## Технологический стек
* **Unity 6000.3.5f2 LTS**
* **Zenject** — Dependency Injection для управления зависимостями.
* **Finite State Machine (FSM)** — управление логикой состояний сцен.
* **Task-System AI** — кастомная система задач для юнитов.
* **NavMesh** — навигация и поиск пути.
* **ScriptableObjects** — конфигурации и спецификации юнитов.

---

## Архитектура и ключевые решения

### 1. Управление глобальными состояниями (Scene FSM)
Основная концепция проекта — управление логикой сцены через (FSM) и единой точки входа для каждой сцены (EntryPoint). Точки входа представлены на каждой игровой сцене в иерархи и именуются согласно названию игровой сцены. Машина состояний поддерживает два типа взаимодействия:
* **Последовательный переход**: завершение текущего состояния и полная очистка контекста перед входом в новое.
* **Приостановка (Suspend)**: текущее состояние ставится на паузу (с сохранением контекста), а поверх него запускается новое. Для этого состояния реализуют расширенный интерфейс `ISuspendFSMState`.

**Для сцены Gameplay определены следующие состояния:**
* `InitState` — инициализация данных и спавн армий.
* `GameLoopState` — основной игровой цикл и расчет сражения.
* `VictoryState` — обработка победы игрока.
* `DefeatState` — обработка поражения.

**Реализация логики переходов в FSMGameplay:**
```csharp
public class FSMGameplay : FSMDictionary<StateGameplay>
{
    ...

    public override void EnterIn(StateGameplay state)
    {
        var nameState = StackStates.TryPeek(out var stateObj) ? $"'{stateObj.GetType().Name}'" : "";
        Debug.Log($"[FSMGameplay] {nameState} -> '{state}'");
    
        base.EnterIn(state);
    }
    
    public override void SuspendAndEnterIn(StateGameplay state)
    {            
        var currentState = StackStates.Peek();
        if (currentState is not ISuspendFSMState<StateGameplay> suspendState)
        {
            ...
        }
    
        var nameState = StackStates.TryPeek(out var stateObj) ? $"'{stateObj.GetType().Name}'" : "";
        Debug.Log($"[FSMGameplay] Приостановка {nameState} ⏸ переход в '{state}'");
    
        base.SuspendAndEnterIn(state);
    }

    ...
}
```

### 2. Внедрение зависимостей (Zenject)
Создание состояний и самой FSM осуществляется через **DI-контейнер Zenject**. Это позволяет избежать прямого прокидывания ссылок и автоматизировать создание обьектов на уровне конструкторов, определенных интерфейсами `IFSMState`, `ISuspendFSMState`.

**Биндинг FSM для Gameplay сцены:**
```csharp
public class GameplayInstaller : MonoInstaller
{
    ...

    public override void InstallBindings()
    {            
        ...

        Container.BindInterfacesAndSelfTo<InitState>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameLoopState>().AsSingle();
        Container.BindInterfacesAndSelfTo<VictoryState>().AsSingle();
        Container.BindInterfacesAndSelfTo<DefeatState>().AsSingle();

        Container.Bind<FSMGameplay>().AsSingle();
    }
}
```

### 4. Система юнитов и спецификаций
Создание армий игрока и противника происходит на основе глобального конфига сформировоной сцецификации `EntitySpecification`, который хранится в конфиги сцены `GameplayConfig` и представляет из себя `ScriptableObject` обьект. Осуществляется конструирование юнита и расчет его конечных параметров фабрикой сущнойсте `EntityFactory` которая так же регестрируется в качестве зависимости в контейнере игровой сцены.

**Фабрика юнитов по спецификациям:**
```csharp
public class EntityFactory
{
    ...

    public Entity Create(Vector3 position, Transform parent, EntitySpecification specifications)
    {
        Entity entity = UnityEngine.Object.Instantiate(_coreEntity, position, parent.rotation, parent);
        var parameters = new EntityParameters(_baseEntityData.HP, _baseEntityData.Speed, _baseEntityData.Damage, _baseEntityData.Cooldown);

        specifications.Specifications.ForEach(spec => spec.Apply(entity, ref parameters));
        entity.SetParameters(parameters);

        return entity;
    }
}
```

* **Customization**: Каждый юнит создается на основе **EntitySpecification** (ScriptableObject). 
* В игре определены **3 спецификации**, влияющие на итоговые параметры (ColorSpecification, MeshSpecification, ScaleSpecification). Списки спецификаций хранятся в `SpecificationsConfig`.

### 5. Task-System Unit AI
Управление юнитами в `GameLoopState` реализовано через **систему выдачи задач**. 
* Каждый юнит — это исполнитель, которому назначается `IEntityTask` (например, `MoveToTask` или `AttackTask`).
* Такая система максимально кастомизируема: добавление новой логики (например, стрельба или лечение) требует лишь создания новой задачи без изменения кода самого юнита.
* Архитектура позволяет управлять большим количеством юнитов одновременно без потери производительности.

**Интерфейс задачи:**
```csharp
public interface IEntityTask
{
    bool IsComplete { get; }
    EntityState State { get; }

    void Start();
    void Update(float timeScale);
    void End();
}
```
* Сама задача назначается сущности, обернутой в соответсвующий класс `FormationEntity`, который в последующем, по мере расширения функционала колод, планируется расширять.

---

### 6. Логика назначения задач
Логика принятия решений о поведении юнитов вынесена в отдельный интерфейс IFormationBehaviour. Это позволяет в перспективе создавать кастомные стратегии как для игрока, так и для оппонента, а также реализовать игровую механику выбора шаблонного поведения арми в качестве мета-геймплея и продавать ее за полученные рессурсы в ходе продвижения игрока.

**Пример механизма назначения задач:**
```csharp
public interface IFormationBehaviour
{
    void Update(Formation thisFormation, Formation enemyFormation, int entityRow, int entityCol);
}

public class DefaultFormationBehaviour : IFormationBehaviour
{
    public void Update(Formation thisFormation, Formation enemyFormation, int entityRow, int entityCol)
    {
        var grid = thisFormation.GetGridEntities();
        var entity = grid[entityRow][entityCol];
        var enemyTarget = FindTargetInFormation(entityRow, entityCol, grid, enemyFormation);

        if (enemyTarget == null) return;

        var distance = Vector3.Distance(entity.Entity.transform.position, enemyTarget.Entity.transform.position);
        var attackRange = entity.Entity.Agent.stoppingDistance + 0.2f;

        if (distance <= attackRange)
        {
            entity.SetTask(new AttackTask(entity, enemyTarget));
        }
        else
        {
            entity.SetTask(new MoveToTask(entity, enemyTarget.Entity.transform));
        }
    }

    ...
}
```

## Геймплейные механики
* **Battle Simulation**: Автоматизированное столкновение армий "стенка на стенку".
* **Time Scale Control**: Реализована система ускорения и замедления геймплея (0.2x, 1.0x) через интерфейс.
* **Army Setup**: Возможность настройки размера армии перед началом боя.
* **Scene Navigation**: Автоматический переход между стартовым экраном и боевой сценой.

