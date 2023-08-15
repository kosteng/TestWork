using System;
using Common;
using Zenject;

namespace UI
{
    public class WindowPresenter : WindowPresenterBase, IInitializable, IDisposable
    {
        private const int AddingAbilityScoreAmount = 1;
        private readonly WindowView _view;
        private readonly IAbilityStateController _stateController;
        private readonly IPlayerAbilityScoreApplicator _scoreApplicator;
        private AbilityItemView _currentAbilityItem;


        public WindowPresenter(WindowView view,
            ICanvasProvider provider,
            IAbilityStateController stateController,
            IPlayerAbilityScoreApplicator scoreApplicator
           ) : base(provider, view)
        {
            _view = view;
            _stateController = stateController;
            _scoreApplicator = scoreApplicator;
        }

        public void Initialize()
        {
            OnAbilityItemClicked(_view.FirstAbilityItem);
            Subscribe();
            RefreshWindowState();
        }
        
        private void Subscribe()
        {
            _view.Subscribe(OnAddScoreButtonClicked, 
                OnAbilityItemClicked, 
                OnLearnClicked, 
                OnForgetClicked, 
                OnForgetAllClicked);
        }

        private void OnLearnClicked()
        {
            _stateController.Learn(_currentAbilityItem.AbilityId);
            _currentAbilityItem.SetLearnState(true);
            RefreshWindowState();
        }

        private void OnForgetAllClicked()
        {
            _stateController.ForgetAll();
            _view.SetForgetAll();
            RefreshWindowState();
        }

        private void OnForgetClicked()
        {
            _stateController.Forget(_currentAbilityItem.AbilityId);
            _currentAbilityItem.SetLearnState(false);
            RefreshWindowState();
        }

        private void OnAbilityItemClicked(AbilityItemView abilityItem)
        {
            if (_currentAbilityItem)
            {
                _currentAbilityItem.SetFocus(false); 
            }
            _currentAbilityItem = abilityItem;
            _currentAbilityItem.SetFocus(true);
            
      
            RefreshWindowState();
            
        }

        private void OnAddScoreButtonClicked()
        {
            _scoreApplicator.Add(AddingAbilityScoreAmount);
            RefreshWindowState();
        }

        public void Dispose()
        {
            _view.Unsubscribe();
        }

        private void RefreshWindowState()
        {
            _view.SetAbilityScoreAmount(_scoreApplicator.CurrentAbilityScore);
            _view.SetButtonsState(_stateController.CanLearn(_currentAbilityItem.AbilityId), 
                _stateController.CanForget(_currentAbilityItem.AbilityId));
        }
    }
}