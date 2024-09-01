using UnityEngine;

namespace DoodleJump
{
    public class BonusHolder : TypedTrigger<Doodler>
    {
        [SerializeField] private Bonus _bonus;

        private void OnEnable()
        {
            TriggerEnter += AddBonusToDoodler;
        }

        private void OnDisable()
        {
            TriggerEnter -= AddBonusToDoodler;
        }

        private void AddBonusToDoodler(Doodler doodler)
        {
            Instantiate(_bonus).Init(doodler);
            Destroy(gameObject);
        }
    }
}