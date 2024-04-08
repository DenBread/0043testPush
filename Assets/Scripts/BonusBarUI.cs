using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LuckyJet
{
    public class BonusBarUI : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private Button _btn;

        public int tapTimer;
        private float _resetTimer = 0.2f;

        private SelectBonusUI _selectBonusUI;
        private BonusDataSave _bonusData;

        [Inject]
        private void Init(SelectBonusUI selectBonusUI)
        {
            _bonusData = SaveLoadSystem.Load<BonusDataSave>();
            _selectBonusUI = selectBonusUI;
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(SwitchBomusBar);
        }
        
        
        private IEnumerator OneTapTime()
        {
            yield return new WaitForSeconds(_resetTimer);
            if(tapTimer == 1)
            {
                _selectBonusUI.OnAdvantage?.Invoke();
                tapTimer = 0;
            }
        }

        private void SwitchBomusBar()
        {
            tapTimer++;
            StartCoroutine(OneTapTime());
            

            if (tapTimer >= 2)
            {
                tapTimer = 0;
                _animator.SetTrigger("ShowCloseBonusBar");
                //Debug.Log("Double click");
            }
            
            
        }
    }
}
