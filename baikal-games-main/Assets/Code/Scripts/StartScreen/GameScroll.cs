using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Rendering;

namespace BaikalGames.StartScreen
{
    public class GameScroll : MonoBehaviour
    {
        public RectTransform startTarget;
        public RectTransform endTarget;
        [SerializeField] private List<GameItem> items;
        [SerializeField] private List<RectTransform> pathItems;
        [SerializeField] private int centerTargetIndex;
        [SerializeField] private Color displayedColor;
        [SerializeField] private Color hiddenColor;
        [SerializeField] private float displayedFontSize = 60;
        [SerializeField] private float hiddenFontSize = 45;
        private int _currentIndex = 0;
        
        public static GameScroll Instance { get; private set; }

        private void OnEnable()
        {
            LeanTouch.OnFingerSwipe += ProcessSwipe;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerSwipe -= ProcessSwipe;
        }

        private void Awake()
        {
            if (Instance) Destroy(this);
            else Instance = this;
        }

        public void Next()
        {
            if (_currentIndex == 0) _currentIndex = items.Count - 1;
            else _currentIndex = (_currentIndex - 1) % items.Count;
            SetTargets(false);
        }

        public void Previous()
        {
            _currentIndex = (_currentIndex + 1) % items.Count;
            SetTargets();
        }

        public void Select(int index, bool next)
        {
            if (_currentIndex == index)
            {
                SetTargets(next); return;
            }

            while (_currentIndex != index)
            {
                if (next) Next();
                else Previous();
            }
        }

        private void SetTargets(bool next = true, bool save = true)
        {
            for (int i = 0; i < items.Count; i++)
            {
                int index = (i + _currentIndex) % items.Count;
                items[i].Target = pathItems[index];
                if (index == centerTargetIndex)
                {
                    items[i].Animator.SetBool("Show", true);
                    items[i].Text.color = displayedColor;
                    items[i].Text.fontSize = displayedFontSize;
                }
                else
                {
                    items[i].Animator.SetBool("Show", false);
                    items[i].Text.color = hiddenColor;
                    items[i].Text.fontSize= hiddenFontSize;
                }
                switch (next)
                {
                    case true when index == 0:
                    case false when index == items.Count - 1:
                        items[i].DuplicateFake(next);
                        break;
                }
            }
            if (save) GameSelectSaver.Save(_currentIndex);
        }

        private void ProcessSwipe(LeanFinger finger)
        {
            if (finger.SwipeScreenDelta.x > 100) Previous();
            else if (finger.SwipeScreenDelta.x < -100) Next();
        }
    }
}
