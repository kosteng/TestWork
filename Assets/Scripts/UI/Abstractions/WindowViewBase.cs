using UnityEngine;

namespace UI
{
    public abstract class WindowViewBase : MonoBehaviour, IWindowView
    {
        public Transform Transform => gameObject.transform;
        
        public void SetParent(Transform parent)
        {
            var rect = (RectTransform)transform;
            rect.SetParent(parent);
            rect.anchoredPosition = Vector2.zero;
        }
    }
}