using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace LuckyJet
{
    public class BestScoreUI : MonoBehaviour
    {
        public Action<int> OnUpdateBestScore;
        [SerializeField] private TextMeshProUGUI _txtScore;
        private ScoreDataSave _scoreData;
        [Inject]
        private void Init()
        {
            _scoreData = SaveLoadSystem.Load<ScoreDataSave>();
            _txtScore.text = _scoreData.BestScore.ToString();

            OnUpdateBestScore += UpdateBestScore;
        }

        private void UpdateBestScore(int score)
        {
            _txtScore.text = score.ToString();
            _scoreData.BestScore = score;
            SaveLoadSystem.Save(_scoreData);
        }

    }
}