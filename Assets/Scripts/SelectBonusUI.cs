using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace LuckyJet
{
    public class SelectBonusUI : MonoBehaviour
    {
        public Action<TypeBonus, int> OnSelect;
        public Action OnAdvantage;
        public TypeBonus TypeBonus { private set; get; }
        [SerializeField] private Image _background;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _txtCoun;
        [SerializeField] private List<BonusProperties> _bonusProperties;

        [Space] 
        [Header("VFX")]
        [SerializeField] private GameObject _tornado;
        [SerializeField] private GameObject _hole;
        [SerializeField] private GameObject _doubleCoin;

        private bool _isActivBonus = true;
        private Button _btn;
        private BonusDataSave _bonusData;
        private SelectBonusDataSave _selectBonusData;
        private ListItemTrigger _itemTriggerSystems;
        private MagnetSystem _magnetSystem;

        [Inject]
        private void Init(ListItemTrigger itemTriggerSystems, MagnetSystem magnetSystem)
        {
            _magnetSystem = magnetSystem;
            _itemTriggerSystems = itemTriggerSystems;
            _btn = GetComponent<Button>();
            _bonusData = SaveLoadSystem.Load<BonusDataSave>();
            _selectBonusData = SaveLoadSystem.Load<SelectBonusDataSave>();
            OnSelect += Select;
            OnAdvantage += Advantage;

            CheckSelectBonus();
        }

        private void Select(TypeBonus typeBonus, int count)
        {
            switch (typeBonus)
            {
                case TypeBonus.Tornado:
                    TypeBonus = TypeBonus.Tornado;
                    _image.sprite = _bonusProperties[0].Sprite;
                    _txtCoun.text = count.ToString();
                    Debug.Log("Запустилось торнадо");
                    break;
                case TypeBonus.Hole:
                    TypeBonus = TypeBonus.Hole;
                    _image.sprite = _bonusProperties[1].Sprite;
                    _txtCoun.text = count.ToString();
                    Debug.Log("Запустилось дырка");
                    break;
                case TypeBonus.DoubleCoin:
                    TypeBonus = TypeBonus.DoubleCoin;
                    _image.sprite = _bonusProperties[2].Sprite;
                    _txtCoun.text = count.ToString();
                    Debug.Log("Запустилось умножние");
                    break;
            }
        }

        private void CheckSelectBonus()
        {
            switch (_selectBonusData.TypeBonus)
            {
                case TypeBonus.Tornado:
                    _image.sprite = _bonusProperties[0].Sprite;
                    _txtCoun.text = _bonusData.Tornado.ToString();
                    break;
                case TypeBonus.Hole:
                    TypeBonus = TypeBonus.Hole;
                    _image.sprite = _bonusProperties[1].Sprite;
                    _txtCoun.text = _bonusData.Hole.ToString();
                    break;
                case TypeBonus.DoubleCoin:
                    TypeBonus = TypeBonus.DoubleCoin;
                    _image.sprite = _bonusProperties[2].Sprite;
                    _txtCoun.text = _bonusData.DoubleCoin.ToString();
                    break;
            }
        }

        private void Advantage()
        {
            if(_isActivBonus == false)
                return;
            
            _bonusData = SaveLoadSystem.Load<BonusDataSave>();
            

            switch (TypeBonus)
            {
                case TypeBonus.Tornado:
                    if (_bonusData.Tornado - 1 >= 0)
                    {
                        _tornado.SetActive(true);
                        _bonusData.Tornado--;
                        ColdownBuff();
                        _selectBonusData.TypeBonus = TypeBonus;
                        _txtCoun.text = _bonusData.Tornado.ToString();
                        foreach (var itemTriggerSystem in _itemTriggerSystems.ListTrigge)
                        {
                            itemTriggerSystem.OnColletionItem?.Invoke();
                        }
                    }

                    break;
                case TypeBonus.Hole:
                    if (_bonusData.Hole - 1 >= 0)
                    {
                        _hole.SetActive(true);
                        _bonusData.Hole--;
                        ColdownBuff();
                        _selectBonusData.TypeBonus = TypeBonus;
                        _txtCoun.text = _bonusData.Hole.ToString();
                        _magnetSystem.gameObject.SetActive(true);
                    }

                    break;
                case TypeBonus.DoubleCoin:
                    if (_bonusData.DoubleCoin - 1 >= 0)
                    {
                        _doubleCoin.SetActive(true);
                        _bonusData.DoubleCoin--;
                        ColdownBuff();
                        _selectBonusData.TypeBonus = TypeBonus;
                        _txtCoun.text = _bonusData.DoubleCoin.ToString();

                        foreach (var itemTriggerSystem in _itemTriggerSystems.ListTrigge)
                        {
                            itemTriggerSystem.OnRation?.Invoke(2);
                        }
                    }

                    break;
            }
            
            SaveLoadSystem.Save(_bonusData);
            SaveLoadSystem.Save(_selectBonusData);
        }

        private void ColdownBuff()
        {
            _isActivBonus = false;
            _background.fillAmount = 0;
            _background.DOFillAmount(1f, 5f)
                .OnComplete(() =>
                {
                    _isActivBonus = true;
                    foreach (var itemTriggerSystem in _itemTriggerSystems.ListTrigge)
                    {
                        itemTriggerSystem.OnRation?.Invoke(1);
                    }
                    _magnetSystem.gameObject.SetActive(false);
                });

        }
    }

    public class SelectBonusDataSave : ISaveData
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeBonus TypeBonus;

        public SelectBonusDataSave()
        {
            TypeBonus = TypeBonus.Tornado;
        }
    }
}
