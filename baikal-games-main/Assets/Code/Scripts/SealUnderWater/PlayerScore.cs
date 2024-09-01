using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace BaikalGames.UnderwaterSealAdvancher
{
    public class PlayerScore : MonoBehaviour
    {
        public float playerScore;
        public int fishCount = 0;

        [SerializeField] private List<TextMeshProUGUI> textScores = new List<TextMeshProUGUI>();
        [SerializeField] private List<TextMeshProUGUI> textFishScore = new List<TextMeshProUGUI>();

        [SerializeField] private Transform player;
        [SerializeField] private TextMeshProUGUI broadfishText,gobyText,golyanText,omulText;
        [SerializeField] private TextMeshProUGUI addFishMessageText;
        [SerializeField] private float rewardPerMeter;
        [SerializeField] private float rewardForBroadfish = 100f, rewardForGoby = 90f, rewardForGolyan = 80f, rewardForOmul = 60f;
        [SerializeField] private ScoreSave scoreSave;

        private List<Fish> _collectedFishList = new List<Fish>();
        private Vector3 _startPosition;
        private float totalScoreForFish;
        
        private void Start()
        {
            _startPosition = player.position;
            playerScore = 0f;
            UpdateFishCount();
            UpdateEnd();
        }
        private void Update()
        {
            playerScore = ((player.position.z - _startPosition.z) * rewardPerMeter) + totalScoreForFish;
            UpdateDistance();
        }
        public void CollectFish(Fish fish)
        {
            _collectedFishList.Add(fish);
            fishCount++;
            float value = 0f;
            switch (fish.type)
            {
                case Fish.FishType.Broadfish:
                    value += rewardForBroadfish; break;
                case Fish.FishType.Goby:
                    value += rewardForGoby; break;
                case Fish.FishType.Golyan:
                    value += rewardForGolyan; break;
                case Fish.FishType.Omul:
                    value += rewardForOmul; break;
            }
            StartCoroutine(ShowFishAddMessage(value));
            totalScoreForFish += value;
            UpdateFishCount();
        }
        private IEnumerator ShowFishAddMessage(float score)
        {
            addFishMessageText.gameObject.SetActive(true);
            addFishMessageText.text = $"+ {score}";
            yield return new WaitForSeconds(0.4f);
            addFishMessageText.gameObject.SetActive(false);
        }
        public void UpdateFishCount()
        {
            foreach (TextMeshProUGUI text in textFishScore)
            {
                text.text = Mathf.FloorToInt(fishCount).ToString();
            }
        }
        public void UpdateEnd()
        {
            int broadfishCount = 0, gobyCount = 0, golyanCount = 0, omulCount = 0;
            foreach (Fish fish in _collectedFishList)
            {
                switch (fish.type)
                {
                    case Fish.FishType.Broadfish:
                        broadfishCount++; break;
                    case Fish.FishType.Goby:
                        gobyCount++; break;
                    case Fish.FishType.Golyan:
                        golyanCount++; break;
                    case Fish.FishType.Omul:
                        omulCount++; break;
                }
            }
            broadfishText.text = broadfishCount.ToString();
            gobyText.text = gobyCount.ToString();
            golyanText.text = golyanCount.ToString();
            omulText.text = omulCount.ToString();

            scoreSave.AddScore(Mathf.FloorToInt(playerScore));
        }
        private void UpdateDistance()
        {
            foreach (TextMeshProUGUI text in textScores)
            {
                text.text = Mathf.FloorToInt(playerScore).ToString();
            }
        }
    }
}
