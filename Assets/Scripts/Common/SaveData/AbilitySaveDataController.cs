using System;
using System.Collections.Generic;
using AbilityConfig;

namespace Common
{
    public class AbilitySaveDataController : IAbilitySaveDataController
    {
        private readonly IAbilityModelsProvider _modelsProvider;
        private Dictionary<int, AbilitySaveData> _saveData = new Dictionary<int, AbilitySaveData>(16);

        public AbilitySaveDataController(IAbilityModelsProvider modelsProvider)
        {
            _modelsProvider = modelsProvider;
            foreach (var (_, model) in modelsProvider.Models)
            {
                if (model.IsBaseAbility)
                {
                    continue;
                }
                _saveData.Add(model.AbilityId, new AbilitySaveData(model.AbilityId, false));
            }
        }

        public bool AbilityHasBeenLearned(int abilityId)
        {
            if (_modelsProvider.GetModel(abilityId).IsBaseAbility)
            {
                return true;
            }
            
            if (_saveData.TryGetValue(abilityId, out var saveData))
            {
                return saveData.AbilityHasBeenLearned;
            }

            throw new Exception($"no ability with id: {abilityId} in save data");
        }

        public List<int> GetLearnedAbilityIds()
        {
            var list = new List<int>();
            foreach (var data in _saveData.Values)
            {
                if (data.AbilityHasBeenLearned)
                {
                    list.Add(data.AbilityId);
                }
            }
            return list;
        }
        
        public void SetAbilityState(int abilityId, bool hasBeenLearned)
        {
            if (_saveData.ContainsKey(abilityId))
            {
                _saveData[abilityId].AbilityHasBeenLearned = hasBeenLearned;
            }
        }
        
        public void ForgetAll()
        {
            foreach (var saveData in _saveData.Values)
            {
                saveData.AbilityHasBeenLearned = false;
            }
        }
    }
}