using Zenject;

namespace Common
{
    public class PlayerAbilityScoreApplicator : IPlayerAbilityScoreApplicator
    {
        public int CurrentAbilityScore => _data.Amount;
        private readonly PlayerAbilityScoreData _data = new PlayerAbilityScoreData();
        
        public void Add(int amount)
        {
            _data.Amount += amount;
        }

        public void Remove(int amount)
        {
            _data.Amount -= amount;
        }
    }
}