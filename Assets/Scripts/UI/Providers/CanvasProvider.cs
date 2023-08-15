using UnityEngine;

namespace UI
{
    public class CanvasProvider : MonoBehaviour, ICanvasProvider
    {
        [SerializeField] private RectTransform _canvas;
    
        public Transform Canvas => _canvas;
    
    }
}