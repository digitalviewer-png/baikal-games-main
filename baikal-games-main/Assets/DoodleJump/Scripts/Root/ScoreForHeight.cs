using UnityEngine;

namespace DoodleJump
{
    public class ScoreForHeight : MonoBehaviour
    {
        [SerializeField] private float _heightMultiplier;

        private Transform _target;
        private float _startHeight;

        public int CurrentPoints { get; private set; }

        public void Init(Transform target)
        {
            _target = target;
            _startHeight = target.transform.position.y;
        }

        private void Update()
        {
            var calculatedHeight = (int)((_target.position.y - _startHeight) * _heightMultiplier);
            CurrentPoints = calculatedHeight > CurrentPoints ? calculatedHeight : CurrentPoints;
        }
    }
}
