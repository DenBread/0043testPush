using DG.Tweening;
using UnityEngine;

namespace LuckyJet
{
    public class PingPongUI : MonoBehaviour
    {
        public float NormalizedValue { get; private set; }
        private float value, minValue = -315f, maxValue = 315f;
        private RectTransform _rectTransform;

        private void OnEnable()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            transform.DOLocalMoveX(315, 1f)
                .From(-315)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }

        private void OnDisable()
        {
            transform.DOKill();
        }

        private void Update()
        {
            value = _rectTransform.anchoredPosition.y;
            NormalizedValue = (value - minValue) / (maxValue - minValue);
        }

        public float GetSpeed()
        {
            this.enabled = false;

            if (NormalizedValue < 0.1)
                NormalizedValue = 0.1f;

            return NormalizedValue;
        }
    }
}