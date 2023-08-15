using System;
using System.Collections.Generic;

namespace AbilityConfig
{
    public class AbilityModelsProvider : IAbilityModelsProvider
    {
        private readonly AbilityModelsConfigAsset _config;
        private Dictionary<int, AbilityModel> _models = new Dictionary<int, AbilityModel>(16);
        public Dictionary<int, AbilityModel> Models => _models;

        public AbilityModelsProvider(AbilityModelsConfigAsset config)
        {
            _config = config;
            foreach (var abilityModel in config.AbilityModels)
            {
                _models.Add(abilityModel.AbilityId, abilityModel);
            }
        }


        public AbilityModel GetModel(int abilityId)
        {
            if (_models.TryGetValue(abilityId, out var model))
            {
                return model;
            }

            throw new  Exception($"no ability with id: {abilityId} in config");
        }
    }
}