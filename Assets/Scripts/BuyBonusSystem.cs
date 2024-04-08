using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LuckyJet
{
    public class BuyBonusSystem : MonoBehaviour
    {
        [SerializeField] private TypeBonus _typeBonus;
        [SerializeField] private int _price;
        [SerializeField] private TextMeshProUGUI _textPrice;
        [SerializeField] private Button _buttonBuy;

        private BonusDataSave _bonusData;
        private CoinDataSave _coinData;

        private CoinUI _coinUI;

        [Inject]
        private void Init(CoinUI coinUI)
        {
            _coinUI = coinUI;
            _buttonBuy.onClick.AddListener(BuyBones);
        }

        private void BuyBones()
        {
            _bonusData = SaveLoadSystem.Load<BonusDataSave>();
            _coinData = SaveLoadSystem.Load<CoinDataSave>();
            
            switch (_typeBonus)
            {
                case TypeBonus.Tornado:
                    if (_coinData.Coin - _price >= 0)
                    {
                        _coinData.Coin -=_price;
                        _bonusData.Tornado++;
                    }
                    break;
                case TypeBonus.DoubleCoin:
                    if (_coinData.Coin - _price >= 0)
                    {
                        _coinData.Coin  -=_price;
                        _bonusData.DoubleCoin++;
                    }
                    break;
                case TypeBonus.Hole:
                    if (_coinData.Coin - _price >= 0)
                    {
                        _coinData.Coin -=_price;
                        _bonusData.Hole++;
                    }
                    break;
                default:
                    Debug.LogError("Ошибка типа бонуса!");
                    break;
            }
            
            SaveLoadSystem.Save(_bonusData);
            SaveLoadSystem.Save(_coinData);
            _coinUI.OnUpdateCoin?.Invoke();
        }
    }

    public enum TypeBonus
    {
        Tornado, DoubleCoin, Hole
    }
}
