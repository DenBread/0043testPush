using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using MelenitasDev.SoundsGood;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace LuckyJet
{
    public class ItemTriggerSystem : MonoBehaviour
    {
        public Action<bool> OnDisable;
        public Action<int> OnRation;
        public Action OnColletionItem;
        [SerializeField] private BoxCollider _boxCollider;
        [SerializeField] private List<GameObject> _listGameObj;
        [SerializeField] private GameObject _item;
        [SerializeField] private GameObject _vfx;
        private List<ScoreUI> _scoreUI;
        private Sound _collectionSound;
        private Vector3 _savePos;
        private bool _isSpawn = true;
        private int _ratio = 1;
        

        [Inject]
        private void Init(List<ScoreUI> scoreUI)
        {
            OnDisable += (i) => _isSpawn = i;
            OnRation += (i) => _ratio = i;
            OnColletionItem += ColletionItem;
            _boxCollider = GetComponent<BoxCollider>();
            _scoreUI = scoreUI;
            _savePos = _item.transform.position;
            
            _collectionSound = new Sound(SFX.CollectingItems);
            _collectionSound
                .SetVolume(0.5f)
                .SetRandomPitch()
                .SetPosition(transform.position)
                .SetOutput(Output.SFX);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<RotationSystem>())
            {
                var vfx = Instantiate(_vfx, transform.position, quaternion.identity);
                var randomScore = Random.Range(10, 100) * _ratio;
                
                
                foreach (var score in _scoreUI)
                {
                    score.OnUpdateScore?.Invoke(randomScore);
                }
                _collectionSound.Play();
                Respawn(3000);
                //_item.SetActive(false);

                _boxCollider.enabled = false;
                foreach (var gm in _listGameObj)
                {
                    gm.SetActive(false);
                }
            }
        }

        public void Spawn(SpriteRenderer spriteRenderer)
        {
            _listGameObj[1].GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
            _item.transform.position = _savePos;
            _boxCollider.enabled = true;
            foreach (var gm in _listGameObj)
            {
                gm.SetActive(true);
            }

            _isSpawn = true;
        }

        private void ColletionItem()
        {
            var vfx = Instantiate(_vfx, transform.position, quaternion.identity);
            var randomScore = Random.Range(10, 100) * _ratio;
                
                
            foreach (var score in _scoreUI)
            {
                score.OnUpdateScore?.Invoke(randomScore);
            }
            _collectionSound.Play();
            Respawn(1000);
            //_item.SetActive(false);

            _boxCollider.enabled = false;
            foreach (var gm in _listGameObj)
            {
                gm.SetActive(false);
            }
        }

        private async void Respawn(int time)
        {
            await Task.Delay(time);
            
            if (Application.isPlaying && _isSpawn == true)
            {
                _boxCollider.enabled = true;
                foreach (var gm in _listGameObj)
                {
                    gm.SetActive(true);
                }
                //_item.SetActive(true);
                _item.transform.position = _savePos;
                _item.transform.DOScale(1, 0.25f)
                    .From(0)
                    .SetEase(Ease.OutBounce);
            }
        }
        
    }

    public class ListItemTrigger
    {
        public List<ItemTriggerSystem> ListTrigge;

        [Inject]
        public ListItemTrigger()
        {
            ListTrigge = new List<ItemTriggerSystem>();
        }
    }
}
