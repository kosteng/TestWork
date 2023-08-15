using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WindowView : WindowViewBase
    {
        [SerializeField] private Button _addAbilityScoreButton;
        [SerializeField] private Button _learnAbilityButton;
        [SerializeField] private Button _forgetAbilityButton;
        [SerializeField] private Button _forgetAllButton;
        [SerializeField] private TextMeshProUGUI _abilityScoreText;
        [SerializeField] private AbilityItemView[] _abilityItemViews;
        public AbilityItemView FirstAbilityItem => _abilityItemViews.First();
        public void Subscribe(Action addScoreClicked, Action<AbilityItemView> abilityItemClicked, Action learnClicked, Action forgetClicked, Action forgetAllClicked)
        {
            _addAbilityScoreButton.onClick.AddListener(() => addScoreClicked?.Invoke());
            _learnAbilityButton.onClick.AddListener(() => learnClicked?.Invoke());
            _forgetAbilityButton.onClick.AddListener(() => forgetClicked?.Invoke());
            _forgetAllButton.onClick.AddListener(() => forgetAllClicked?.Invoke());
            
            foreach (var view in _abilityItemViews)
            {
                view.Subscribe(abilityItemClicked);
            }
        }

        public void SetAbilityScoreAmount(int scoreAmount)
        {
            _abilityScoreText.text = $"Current ability score: {scoreAmount.ToString()}";
        }
        
        public void Unsubscribe()
        {
            _addAbilityScoreButton.onClick.RemoveAllListeners();
            foreach (var view in _abilityItemViews)
            {
                view.Unsubscribe();
            }
        }

        public void SetButtonsState(bool canLearn, bool canForget)
        {
            _learnAbilityButton.enabled = canLearn;
            _learnAbilityButton.interactable = canLearn;
            _forgetAbilityButton.enabled = canForget;
            _forgetAbilityButton.interactable = canForget;
        }

        public void SetForgetAll()
        {
            foreach (var view in _abilityItemViews)
            {
                view.SetLearnState(false);
            }
        }
    }
}