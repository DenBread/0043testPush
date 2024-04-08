using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MelenitasDev.SoundsGood;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyJet
{
    public class BtnAnim : MonoBehaviour
    {
        private Button _btn;
        private Vector3 _saveScale;
        private Sound _btnSound;

        private void Start()
        {
            _btnSound = new Sound(SFX.BtnSound);
            _btnSound
                .SetVolume(0.5f)
                .SetRandomPitch()
                .SetPosition(transform.position)
                .SetOutput(Output.SFX);
            
            _saveScale = transform.localScale;
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(Anim);
        }

        private void Anim()
        {
            _btnSound.Play();
            transform.localScale = _saveScale;
            transform.DOKill();
            
            transform.DOScale(_saveScale * 0.8f, 0.1f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.InSine)
                .OnComplete(() =>
                {
                    transform.localScale = _saveScale;
                });
        }
    }
}
