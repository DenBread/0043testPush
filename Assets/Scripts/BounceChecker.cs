using System;
using System.Collections;
using System.Collections.Generic;
using MelenitasDev.SoundsGood;
using UnityEngine;

namespace LuckyJet
{
    public class BounceChecker : MonoBehaviour
    {
        private Sound _hittingWallSound;

        private DynamicMusic _dynamicMusic;

        private void Start()
        {
            
            _hittingWallSound = new Sound(SFX.HittingWall);
            _hittingWallSound
                .SetVolume(0.1f)
                .SetRandomPitch()
                .SetPosition(transform.position)
                .SetFadeOut(0.1f)
                .SetOutput(Output.SFX);
        }

        private void OnCollisionEnter(Collision other)
        {
            _hittingWallSound.Play();
            Debug.Log("Сталкнулся");
        }
    }
}
