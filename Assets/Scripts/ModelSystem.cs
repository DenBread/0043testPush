using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LuckyJet
{
    public class ModelSystem : MonoBehaviour
    {
        [SerializeField] private List<VolchekProperties> _volchekProperties;
        
        [Space]
        [SerializeField] private Button _leftModel, _rightModel;
        [SerializeField] private Image _imgModel;
        [SerializeField] private TextMeshProUGUI _textLevel;
        
        [Space]
        [SerializeField] private Button _leftAppearance, _rightAppearance;
        [SerializeField] private Image _imgAppearance;

        private int _indexModel;
        private int _indexAppearance;

        private VolchekDataSave _volchekData;

        [Inject]
        private void Init()
        {
            _leftModel.onClick.AddListener(()=>SelectModel(-1));
            _rightModel.onClick.AddListener(()=>SelectModel(1));
            
            _leftAppearance.onClick.AddListener(()=>SelectAppearance(-1));
            _rightAppearance.onClick.AddListener(()=>SelectAppearance(1));

            _volchekData = SaveLoadSystem.Load<VolchekDataSave>();
        }

        private void SelectModel(int i = 0)
        {
            _indexModel += i;

            if (_indexModel > _volchekProperties.Count - 1)
            {
                _indexModel = 0;
            }
            if(_indexModel < 0)
            {
                _indexModel = _volchekProperties.Count - 1;
            }

            SetColorText();
            
            Debug.Log("Index: " + _indexModel);
            
            _imgModel.sprite = _volchekProperties[_indexModel].ModelVolchek;
            _imgAppearance.sprite = _volchekProperties[_indexModel].ListVolchek[_indexAppearance];
        }

        private void SetColorText()
        {
            switch (_indexModel)
            {
                case 0:
                    _textLevel.text = "Easy";
                    _textLevel.color = Color.green;
                    break;
                case 1:
                    _textLevel.text = "Average";
                    _textLevel.color = Color.yellow;
                    break;
                case 2:
                    _textLevel.text = "Hard";
                    _textLevel.color = Color.red;
                    break;
            }
        }

        private void SelectAppearance(int i = 0)
        {
            _indexAppearance += i;

            if (_indexAppearance > _volchekProperties[_indexModel].ListVolchek.Count - 1)
            {
                _indexAppearance = 0;
            }
            if(_indexAppearance < 0)
            {
                _indexAppearance = _volchekProperties[_indexModel].ListVolchek.Count - 1;
            }
            
            Debug.Log("_imgAppearance: " + _imgAppearance);
            
            _imgAppearance.sprite = _volchekProperties[_indexModel].ListVolchek[_indexAppearance];
            
            _volchekData.IndexAppearance[_indexModel] = _indexAppearance;
            SaveLoadSystem.Save(_volchekData);
        }
    }
}
