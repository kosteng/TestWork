using UnityEngine;

namespace UI
{
    public interface IWindowView
    {
        Transform Transform { get; }
        void SetParent(Transform parent);
    }
}