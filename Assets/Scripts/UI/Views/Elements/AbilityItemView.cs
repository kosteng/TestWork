using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AbilityItemView : MonoBehaviour
    {
        [field: SerializeField] public int AbilityId { get; private set; }
        [SerializeField] private Button _itemButton;
        [SerializeField] private Color _hasBeenLearnedColor;
        [SerializeField] private Color _hasNotBeenLearnedColor;
        [SerializeField] private GameObject _focusImage;
        

        public void Subscribe(Action<AbilityItemView> itemClicked)
        {
            _itemButton.onClick.AddListener(() => itemClicked.Invoke(this));
        }

        public void SetLearnState(bool hasBeenLearned)
        {
            _itemButton.image.color = hasBeenLearned ? _hasBeenLearnedColor : _hasNotBeenLearnedColor;
        }

        public void SetFocus(bool isActive)
        {
            _focusImage.SetActive(isActive);
        }

        public void Unsubscribe()
        {
            _itemButton.onClick.RemoveAllListeners();
        }
    }
}