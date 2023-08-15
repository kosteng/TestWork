using System;

namespace Common
{
    [Serializable]
    public class AbilitySaveData
    {
        public int AbilityId { get; }
        public bool AbilityHasBeenLearned;

        public AbilitySaveData(int abilityId, bool abilityHasBeenLearned)
        {
            AbilityId = abilityId;
            AbilityHasBeenLearned = abilityHasBeenLearned;
        }
    }
}