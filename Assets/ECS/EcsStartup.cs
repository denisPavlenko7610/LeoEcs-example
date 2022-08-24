using Client.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        
        [SerializeField] EcsUguiEmitter _uiEmitter;
        
        EcsWorld _world;        
        IEcsSystems _systems;

        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _systems
                .Add(new DetectCollisionSystem())
                .Add(new TestUI())
                .Add(new DetectTargetSystem())
                .Add(new MoveToTargetSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .InjectUgui(_uiEmitter)
                .ConvertScene()
                .Init ();
        }

        void Update () {
            // process systems here.
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                //_systems.GetWorld ("ugui-events").Destroy ();
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}