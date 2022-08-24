using Client.Components;
using Leopotam.EcsLite;

namespace Client.Systems
{
    public class MoveToTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter filterPlayer;
        private EcsWorld world;
        private EcsPool<TargetComponent> poolTarget;
        private EcsPool<NavigationComponent> poolNavigation;
        private EcsFilter filterEnemy;
        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filterEnemy = world.Filter<EnemyTag>().Inc<NavigationComponent>().End();
            poolNavigation = world.GetPool<NavigationComponent>();
            poolTarget = world.GetPool<TargetComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filterEnemy)
            {
                if (poolTarget.Has(entity) && poolNavigation.Has(entity))
                {
                    var target = poolTarget.Get(entity).Value.position;
                    poolNavigation.Get(entity).NavMeshAgent.SetDestination(target);
                }
            }
        }
    }
}