using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump
{
    public class FactsSystem : MonoBehaviour
    {
        [SerializeField] private List<FactCharacter> _leftChars = new List<FactCharacter>();
        [SerializeField] private List<FactCharacter> _rightChars = new List<FactCharacter>();
        [SerializeField] private float _timeBetweenCharactersChange = 6f;
        [SerializeField] private float _timeWithoutCharacters = 0.5f;

        private void OnEnable()
        {
            StartCoroutine(FactsRoutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator FactsRoutine()
        {
            foreach (var factCharacter in _leftChars)
            {
                factCharacter.Hide();
            }

            foreach (var factCharacter in _rightChars)
            {
                factCharacter.Hide();
            }

            while (true)
            {
                var currentLeftChar = _leftChars[Random.Range(0, _leftChars.Count)];
                var currentRightChar = _rightChars[Random.Range(0, _leftChars.Count)];

                currentLeftChar.Show();
                currentRightChar.Show();

                yield return new WaitForSeconds(_timeBetweenCharactersChange);

                currentLeftChar.Hide();
                currentRightChar.Hide();
                
                yield return new WaitForSeconds(_timeWithoutCharacters);
            }
        }
    }
}