using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace LuckyJet
{
    public class CoinUI : MonoBehaviour
    {
        public Action OnUpdateCoin;
        [SerializeField] private TextMeshProUGUI _txtCoin;
        private CoinDataSave _coinData;

        [Inject]
        private void Init()
        {
            UpdateCoin();
            OnUpdateCoin += UpdateCoin;
        }

        private void UpdateCoin()
        {
            _coinData = SaveLoadSystem.Load<CoinDataSave>();
            _txtCoin.text = _coinData.Coin.ToString();
        }
    }
}
