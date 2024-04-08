using System.Collections;
using System.Collections.Generic;
using MelenitasDev.SoundsGood;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LuckyJet
{
    public class RetryBtn : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        
        private Button _btn;
        
        private SpawerSystem _spawerSystem;
        private RotationSystem _rotationSystem;
        private List<ScoreUI> _scoreUI;
        private SelectLevelSystem _selectLevelSystem;
        private BtnPushVolchek _btnPushVolchek;
        
        
        [Inject]
        private void Init(SpawerSystem spawerSystem, RotationSystem rotationSystem, List<ScoreUI> scoreUI, SelectLevelSystem selectLevelSystem, BtnPushVolchek btnPushVolchek)
        {
            _btn = GetComponent<Button>();
            
            
            _spawerSystem = spawerSystem;
            _rotationSystem = rotationSystem;
            _scoreUI = scoreUI;
            _selectLevelSystem = selectLevelSystem;
            _btnPushVolchek = btnPushVolchek;
            
            _btn.onClick.AddListener(Retry);
        }

        private void Retry()
        {
            _panel.SetActive(false);
            _spawerSystem.Respawn();
            _rotationSystem.ResetPostion();
            _selectLevelSystem.LoadMap();
            _btnPushVolchek.gameObject.SetActive(true);

            foreach (var scoreUI in _scoreUI)
            {
                scoreUI.Reset();
            }
        }
    }
}
