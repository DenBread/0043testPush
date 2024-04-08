using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LuckyJet
{
    public class BtnPushVolchek : MonoBehaviour
    {
        
        private Button _button;
        private RotationSystem _rotationSystem;

        [Inject]
        private void Init(RotationSystem rotationSystem)
        {
            _button = GetComponent<Button>();
            _rotationSystem = rotationSystem;
            
            _button.onClick.AddListener(_rotationSystem.Push);
            _button.onClick.AddListener(()=>gameObject.SetActive(false));
        }
    }
}