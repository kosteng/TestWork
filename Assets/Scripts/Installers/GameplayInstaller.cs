using AbilityConfig;
using Common;
using UI;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayer();
            BindAbilities();
            BindWindows();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesTo<PlayerAbilityScoreApplicator>().AsSingle().NonLazy();
        }

        private void BindWindows()
        {
            Container.BindInterfacesAndSelfTo<WindowPresenter>().AsSingle().NonLazy();
        }

        private void BindAbilities()
        {
            Container.BindInterfacesTo<AbilityModelsProvider>().AsSingle();
            Container.BindInterfacesTo<AbilityStateController>().AsSingle();
            Container.BindInterfacesTo<AbilitySaveDataController>().AsSingle();
        }
    }
}