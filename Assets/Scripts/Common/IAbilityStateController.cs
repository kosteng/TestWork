namespace Common
{
    public interface IAbilityStateController
    {
        bool CanLearn(int abilityId);
        bool CanForget(int abilityId);
        void Forget(int abilityId);
        void Learn(int abilityId);
        void ForgetAll();
    }
}