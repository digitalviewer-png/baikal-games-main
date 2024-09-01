using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DoodleJump
{
    public class RespawnPanelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private UiShowingAnimation _showingAnimation;

        [Space]
        [SerializeField] private AnswerView _answerPrefab;
        [SerializeField] private Transform _answersParent;

        private List<AnswerView> _currentAnswers = new List<AnswerView>();
        private QuestionsHolder.Question _currentQuestion;

        public Action<QuestionsHolder.Question, bool> Answered;

        public void Open(QuestionsHolder.Question question)
        {
            _showingAnimation.Show();
            _questionText.text = question.QuestionText;

            _currentQuestion = question;

            ClearAnswers();
            CreateAnswers(question.AnswersTexts);
        }

        public void Close()
        {
            _showingAnimation.Hide();
        }

        private void OnAnswerClicked(AnswerView answer)
        {
            foreach (var currentAnswer in _currentAnswers)
            {
                currentAnswer.Block();
            }

            var answerIndex = _currentAnswers.IndexOf(answer);

            if (_currentQuestion.IsAnswerRight(answerIndex))
            {
                answer.SetRight(true);
            }
            else
            {
                answer.SetRight(false);

                for (int i = 0; i < _currentAnswers.Count; i++)
                {
                    if (_currentQuestion.IsAnswerRight(i))
                        _currentAnswers[i].SetRight(true);
                }
            }

            StartCoroutine(InvokeRoutine(answerIndex));
        }

        private void CreateAnswers(string[] answers)
        {
            foreach (var answer in answers)
            {
                var newView = Instantiate(_answerPrefab, _answersParent);
                newView.Init(answer);
                newView.Clicked += OnAnswerClicked;

                _currentAnswers.Add(newView);
            }
        }

        private void ClearAnswers()
        {
            foreach (var currentAnswer in _currentAnswers)
            {
                currentAnswer.Clicked -= OnAnswerClicked;
                Destroy(currentAnswer.gameObject);
            }

            _currentAnswers.Clear();
        }

        private IEnumerator InvokeRoutine(int index)
        {
            yield return new WaitForSeconds(1f);
            Answered?.Invoke(_currentQuestion, _currentQuestion.IsAnswerRight(index));
        }
    }
}