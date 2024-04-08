using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LuckyJet
{
    public class BonusUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtCount;
        [SerializeField] private TypeBonus _typeBonus;

        private Button _btn;
        private BonusDataSave _bonusData;
        private SelectBonusUI _selectBonusUI;

        [Inject]
        private void Init(SelectBonusUI selectBonusUI)
        {
            _selectBonusUI = selectBonusUI;
            _btn = GetComponent<Button>();
            _bonusData = SaveLoadSystem.Load<BonusDataSave>();
            _btn.onClick.AddListener(Select);
            
            switch (_typeBonus)
            {
                case TypeBonus.Tornado:
                    _txtCount.text = _bonusData.Tornado.ToString();
                    break;
                case TypeBonus.Hole:
                    _txtCount.text = _bonusData.Hole.ToString();
                    break;
                case TypeBonus.DoubleCoin:
                    _txtCount.text = _bonusData.DoubleCoin.ToString();
                    break;
            }
        }

        private void Select()
        {
            switch (_typeBonus)
            {
                case TypeBonus.Tornado:
                    _txtCount.text = _bonusData.Tornado.ToString();
                    _selectBonusUI.OnSelect?.Invoke(TypeBonus.Tornado, _bonusData.Tornado);
                    break;
                case TypeBonus.Hole:
                    _txtCount.text = _bonusData.Hole.ToString();
                    _selectBonusUI.OnSelect?.Invoke(TypeBonus.Hole, _bonusData.Hole);
                    break;
                case TypeBonus.DoubleCoin:
                    _txtCount.text = _bonusData.DoubleCoin.ToString();
                    _selectBonusUI.OnSelect?.Invoke(TypeBonus.DoubleCoin, _bonusData.DoubleCoin);
                    break;
            }
        }
    }
}
