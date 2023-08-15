using AbilityConfig;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create PrefabInstaller", fileName = "PrefabInstaller")]
    public class PrefabInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CanvasProvider _canvasProvider;
        [SerializeField] private WindowView _windowView;
        [SerializeField] private AbilityModelsConfigAsset _abilityModelsConfigAsset;

        public override void InstallBindings()
        {
            Container.Bind<ICanvasProvider>().FromComponentInNewPrefab(_canvasProvider).AsSingle().NonLazy();
            Container.Bind<WindowView>().FromComponentInNewPrefab(_windowView).AsSingle();
            Container.BindInstance(_abilityModelsConfigAsset);
        }
    }
}