namespace Common
{
    public interface IPlayerAbilityScoreApplicator
    {
        int CurrentAbilityScore { get; }
        void Add(int amount);
        void Remove(int amount);
    }
}