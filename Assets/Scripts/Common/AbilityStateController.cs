using System.Linq;
using AbilityConfig;

namespace Common
{
    public class AbilityStateController : IAbilityStateController
    {
        private readonly IAbilitySaveDataController _saveDataController;
        private readonly IPlayerAbilityScoreApplicator _scoreApplicator;
        private readonly IAbilityModelsProvider _modelsProvider;

        public AbilityStateController(IAbilitySaveDataController saveDataController,
            IPlayerAbilityScoreApplicator scoreApplicator,
            IAbilityModelsProvider modelsProvider)
        {
            _saveDataController = saveDataController;
            _scoreApplicator = scoreApplicator;
            _modelsProvider = modelsProvider;
        }

        public bool CanLearn(int abilityId)
        {
            var model = _modelsProvider.GetModel(abilityId);
            if (model.IsBaseAbility)
            {
                return false;
            }

            if (_saveDataController.AbilityHasBeenLearned(abilityId))
            {
                return false;
            }

            var canLearn = _scoreApplicator.CurrentAbilityScore >= model.AbilityScoreCost;

            if (!canLearn)
            {
                return false;
            }

            var parentHasBeenLearned = ParentHasBeenLearned(model);

            return parentHasBeenLearned;
        }

        public void Learn(int abilityId)
        {
            var model = _modelsProvider.GetModel(abilityId);
            _scoreApplicator.Remove(model.AbilityScoreCost);
            _saveDataController.SetAbilityState(abilityId, true);
        }

        public bool CanForget(int abilityId)
        {
            
            if (!_saveDataController.AbilityHasBeenLearned(abilityId))
            {
                return false;
            }

            var model = _modelsProvider.GetModel(abilityId);
          
            if (model.IsBaseAbility)
            {
                return false;
            }
            
            if (model.ChildrenId.Length < 1)
            {
                return true;
            }

            if (ParentIsBaseAbility(model))
            {
                return true;
            }

            if (HasNotBeenLearnedChildren(model))
            {
                return true;
            }
            
            var canForget = CanForget(abilityId, model);


            return canForget;
        }

        public void Forget(int abilityId)
        {
            var model = _modelsProvider.GetModel(abilityId);
            _scoreApplicator.Add(model.AbilityScoreCost);
            _saveDataController.SetAbilityState(abilityId, false);
        }

        public void ForgetAll()
        {
            var learnedIds = _saveDataController.GetLearnedAbilityIds();

            foreach (var learnedId in learnedIds)
            {
                var model = _modelsProvider.GetModel(learnedId);
                _scoreApplicator.Add(model.AbilityScoreCost);
            }

            _saveDataController.ForgetAll();
        }

        private bool HasNotBeenLearnedChildren(AbilityModel model)
        {
            var hasNotBeenLearned = false;
            foreach (var childId in model.ChildrenId)
            {
                if (!_saveDataController.AbilityHasBeenLearned(childId))
                {
                    hasNotBeenLearned = true;
                }
                else
                {
                    hasNotBeenLearned = false;
                    break;
                }
            }

            return hasNotBeenLearned;
        }
        
        private bool CanForget(int abilityId, AbilityModel model)
        {
            var canForget = false;
            foreach (var childId in model.ChildrenId)
            {
                var childModel = _modelsProvider.GetModel(childId);
                
                foreach (var parentId in childModel.ParentsId)
                {
                    if (parentId == abilityId)
                    {
                        canForget = false;
                    }
                    else
                    {
                        if (_saveDataController.AbilityHasBeenLearned(parentId))
                        {
                            canForget = true;
                            break;
                        }
                    }
                }
            }

            return canForget;
        }

        private bool ParentHasBeenLearned(AbilityModel model)
        {
            foreach (var id in model.ParentsId)
            {
                if (_modelsProvider.GetModel(id).IsBaseAbility)
                {
                    return true;
                }

                var parentDataSave = _saveDataController.AbilityHasBeenLearned(id);

                if (!parentDataSave) continue;
                return true;
            }

            return false;
        }

        private bool ParentIsBaseAbility(AbilityModel model)
        {
            return model.ParentsId.Select(parentId => _modelsProvider.GetModel(parentId))
                .Any(parentModel => parentModel.IsBaseAbility) && HasNotBeenLearnedChildren(model);
        }
    }
}