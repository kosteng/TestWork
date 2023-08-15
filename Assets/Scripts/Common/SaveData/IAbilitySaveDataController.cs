using System.Collections.Generic;

namespace Common
{
    public interface IAbilitySaveDataController
    {
        void SetAbilityState(int abilityId, bool hasBeenLearned);
        bool AbilityHasBeenLearned(int abilityId);
        List<int> GetLearnedAbilityIds();
        void ForgetAll();
    }
}