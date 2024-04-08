using System.Collections;
using System.Collections.Generic;
using MelenitasDev.SoundsGood;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LuckyJet
{
    public class HomeBtn : MonoBehaviour
    {
        [SerializeField] private GameObject _panelUI;
        [SerializeField] private GameObject _menuUI;
        [SerializeField] private GameObject _gameplayUI;
        [SerializeField] private GameObject _volchek;
        [SerializeField] private GameObject _ground;
        [SerializeField] private List<GameObject> _maps;
        
        private Music _musicMenu;

        private Button _btn;
        
        [Inject]
        private void Init()
        {
            _musicMenu = new Music(Track.MusicMenu);
            _musicMenu
                .SetVolume(0.8f)
                .SetFadeOut(1f)
                .SetOutput(Output.Music)
                .SetLoop(true);
            
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(Home);
        }

        private void Home()
        {
            AudioManager.StopAllMusic();
            _musicMenu.Play();
            _panelUI.SetActive(false);
            _menuUI.SetActive(true);
            _gameplayUI.SetActive(false);
            _volchek.SetActive(false);
            _ground.SetActive(false);

            foreach (var map in _maps)
            {
                map.SetActive(false);
            }
        }
    }
}
