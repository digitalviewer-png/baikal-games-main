using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DoodleJump
{
    public class QuestionsHolder : MonoBehaviour
    {
        [Serializable]
        public class Question
        {
            [TextArea] [SerializeField] private string _questionText;
            [TextArea] [SerializeField] private string _fact;
            [TextArea] [SerializeField] private string[] _answersTexts;
            [SerializeField] private List<int> _rightAnswers = new List<int>();

            public string QuestionText => _questionText;
            public string Fact => _fact;
            public string[] AnswersTexts => _answersTexts;

            public bool IsAnswerRight(int answer)
            {
                return _rightAnswers.Contains(answer);
            }
        }

        [SerializeField] private Question[] _questions;

        public Question GetRandomQuestion()
        {
            return _questions[Random.Range(0, _questions.Length)];
        }
    }
}
