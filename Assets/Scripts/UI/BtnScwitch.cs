using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MelenitasDev.SoundsGood;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyJet
{
    public class BtnScwitch : MonoBehaviour
    {
        [SerializeField] private SoundType _soundType;
        [SerializeField] private RectTransform _btnImg;
        private Button _btn;
        private bool _isOnOff = false;
        

        private void Start()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(Switcher);
            Switcher();
        }

        private void Switcher()
        {
            _isOnOff = !_isOnOff;
            
            if (_isOnOff == true)
            {
                _btnImg.DOLocalMoveX(70f, 0.25f);

                if (_soundType == SoundType.SFX)
                {
                    AudioManager.ChangeOutputVolume(Output.SFX, 1);
                }
                else
                {
                    AudioManager.ChangeOutputVolume(Output.Music, 1);
                }
                
            }
            else
            {
                _btnImg.DOLocalMoveX(-70f, 0.25f);

                if (_soundType == SoundType.SFX)
                {
                    AudioManager.ChangeOutputVolume(Output.SFX, 0);
                }
                else
                {
                    AudioManager.ChangeOutputVolume(Output.Music, 0);
                }
            }

            
        }
    }

    public enum SoundType
    {
        SFX, Music
    }
}
