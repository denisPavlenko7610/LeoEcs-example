using Client.Components;
using Leopotam.EcsLite;

namespace Client.Systems
{
    public class DetectTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter filterPlayer;
        private EcsWorld world;
        private EcsPool<TargetComponent> poolTarget;
        private EcsPool<TransformComponent> poolTransform;
        private EcsFilter filterEnemy;
        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filterPlayer = world.Filter<PlayerTag>()
                .Inc<TransformComponent>()
                .End();
            
            filterEnemy = world.Filter<EnemyTag>().End();
            poolTarget = world.GetPool<TargetComponent>();
            poolTransform = world.GetPool<TransformComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in filterPlayer)
            {
                var playerTransform = poolTransform.Get(playerEntity).Value.transform;

                foreach (var enemyEntity in filterEnemy)
                {
                    if (poolTarget.Has(enemyEntity) == false)
                    {
                        poolTarget.Add(enemyEntity).Value = playerTransform;
                    }
                }
            }
        }
    }
}