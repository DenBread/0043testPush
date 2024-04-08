using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace LuckyJet
{
    public class ScoreUI : MonoBehaviour
    {
        public Action<int> OnUpdateScore;
        [SerializeField] private TextMeshProUGUI _txtScore;
        private int _score;
        
        private ScoreDataSave _scoreData;
        private CoinDataSave _coinData;
        
        private BestScoreUI _bestScoreUI;
        private CoinUI _coinUI;

        [Inject]
        private void Init(BestScoreUI bestScoreUI, CoinUI coinUI)
        {
            _bestScoreUI = bestScoreUI;
            _coinUI = coinUI;
            _scoreData = SaveLoadSystem.Load<ScoreDataSave>();
            _coinData = SaveLoadSystem.Load<CoinDataSave>();
            UpdateScore(0);
            OnUpdateScore += UpdateScore;
        }

        private void UpdateScore(int score)
        {
            _score += score;
            _txtScore.text = _score.ToString();

            if (_scoreData.BestScore < _score)
            {
                _bestScoreUI.OnUpdateBestScore?.Invoke(_score);
            }
        }

        public void Reset()
        {
            _coinUI.OnUpdateCoin?.Invoke();
            _coinData.Coin = _score / 2;
            SaveLoadSystem.Save(_coinData);
            
            _score = 0;
            _txtScore.text = _score.ToString();
        }
        
        // private void UpdateScore()
        // {
        //     _coinData = SaveLoadSystem.Load<ScoreDataSave>();
        //     _txtCoin.text = _coinData.Coin.ToString();
        // }
    }
}
