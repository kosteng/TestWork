using System.Collections.Generic;

namespace AbilityConfig
{
    public interface IAbilityModelsProvider
    {
        Dictionary<int, AbilityModel> Models { get; }
        AbilityModel GetModel(int abilityId);
    }
}