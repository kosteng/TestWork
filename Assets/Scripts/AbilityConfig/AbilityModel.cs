using System;
using UnityEngine;

namespace AbilityConfig
{
    [Serializable]
    public class AbilityModel
    {
        [field: SerializeField] public int AbilityId { get; private set; }
        [field: SerializeField] public int AbilityScoreCost { get; private set; }
        [field: SerializeField] public int[] ParentsId { get; private set; }
        [field: SerializeField] public int[] ChildrenId { get; private set; }
        [field: SerializeField] public bool IsBaseAbility { get; private set; }
    }
}