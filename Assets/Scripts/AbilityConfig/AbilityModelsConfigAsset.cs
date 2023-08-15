using UnityEngine;

namespace AbilityConfig
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create AbilityConfigAsset", fileName = "AbilityConfigAsset")]
    public class AbilityModelsConfigAsset : ScriptableObject
    {
        [field: SerializeField] public AbilityModel[] AbilityModels { get; private set; }
    }
}