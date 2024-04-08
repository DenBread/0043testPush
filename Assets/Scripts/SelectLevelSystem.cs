using System.Collections.Generic;
using MelenitasDev.SoundsGood;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace LuckyJet
{
    public class SelectLevelSystem : MonoBehaviour
    {
        [Header("Select panel UI")]
        [SerializeField] private Button _btnPlay;
        
        [SerializeField] private Button _leftModel;
        [SerializeField] private Button _rightModel;
        
        [SerializeField] private Image _imgModel;
        [SerializeField] private TextMeshProUGUI _textLevel;
        
        [SerializeField] private List<Image> _imgMapList;
        [SerializeField] private GameObject _menuUI;
        [SerializeField] private GameObject _gameplayUI;

        [Space]
        [Header("Game item")]
        [SerializeField] private List<GameObject> _listMap;

        [SerializeField] private SpriteRenderer _imageVolchek;
        [SerializeField] private GameObject _volchek;
        [SerializeField] private GameObject _background;
        

        [SerializeField] private List<VolchekProperties> _volchekProperties;
        private int _indexModel;
        private VolchekDataSave _volchekData;
        private int _indexMap;
        
        private Music _musicMenu;

        [Inject]
        private void Init()
        {
            _musicMenu = new Music(Track.MusicGame);
            _musicMenu
                .SetVolume(0.8f)
                .SetFadeOut(0.5f)
                .SetOutput(Output.Music)
                .SetLoop(true);
            
            var list = Resources.LoadAll<VolchekProperties>("Volcheks");
            _volchekProperties.AddRange(list);
            
            _leftModel.onClick.AddListener(()=>SelectModel(-1));
            _rightModel.onClick.AddListener(()=>SelectModel(1));
            _btnPlay.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            AudioManager.StopAllMusic();
            _musicMenu.Play();
            Debug.Log("Start game");
            _volchekData = SaveLoadSystem.Load<VolchekDataSave>();
            this.gameObject.SetActive(false);
            _menuUI.gameObject.SetActive(false);
            _background.SetActive(true);
            _gameplayUI.SetActive(true);
            _volchek.SetActive(true);
            _imageVolchek.sprite = _volchekProperties[_indexModel].ListVolchek[_volchekData.IndexAppearance[_indexModel]];

            LoadMap();
        }

        public void LoadMap()
        {
            foreach (var map in _listMap)
            {
                map.SetActive(false);
            }
            
            switch (_indexModel)
            {
                case 0:
                    _indexMap = Random.Range(0, 2);
                    _listMap[_indexMap].SetActive(true);
                    break;
                case 1:
                    _indexMap = Random.Range(2, 4);
                    _listMap[_indexMap].SetActive(true);
                    break;
                case 2:
                    _indexMap = Random.Range(4, 6);
                    _listMap[_indexMap].SetActive(true);
                    break;
                default:
                    _indexMap = Random.Range(0, 2);
                    _listMap[_indexMap].SetActive(true);
                    break;
            }
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
            
            _imgModel.sprite = _volchekProperties[_indexModel].ModelVolchek;

            for (int j = 0; j < _volchekProperties[_indexModel].ListMap.Count; j++)
            {
                _imgMapList[j].sprite = _volchekProperties[_indexModel].ListMap[j];
            }
            //_imgAppearance.sprite = _volchekProperties[_indexModel].ListVolchek[_indexAppearance];
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
    }
}
