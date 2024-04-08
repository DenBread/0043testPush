using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace LuckyJet
{
    public class SpawerSystem : MonoBehaviour
    {
        [SerializeField] private List<Transform> _listPoint;
        [SerializeField] private List<GameObject> _listItem;

        private ListItemTrigger _listItemTrigger;
        
        private DiContainer _diContainer;

        [Inject]
        private void Init(DiContainer diContainer, ListItemTrigger listItemTrigger)
        {
            _listItemTrigger = listItemTrigger;
            _diContainer = diContainer;
            var items = Resources.LoadAll<GameObject>("Items");
            _listItem.AddRange(items);

            for (int i = 0; i < _listPoint.Count; i++)
            {
                var point = _diContainer.InstantiatePrefabForComponent<ItemNew>(_listItem[Random.Range(0,_listItem.Count)], _listPoint[i].transform.position, quaternion.identity, transform);
                //_listGameobjectsItem.Add(point.gameObject);
                listItemTrigger.ListTrigge.Add(point.ItemTriggerSystem);
                //_diContainer.InjectGameObject(point);
                //_diContainer.Bind<ItemTriggerSystem>().FromInstance(point).AsSingle().NonLazy();
            }

            

        }

        public void Respawn()
        {
            // foreach (var item in _listItemTrigger.ListTrigge)
            // {
            //     Destroy(item);
            // }
            
            for (int i = 0; i < _listItemTrigger.ListTrigge.Count; i++)
            {
                //_listItemTrigger.ListTrigge[i].transform.parent.gameObject.SetActive(true);
                //_listItemTrigger.ListTrigge[i] = _diContainer.InstantiatePrefab(_listItem[Random.Range(0,_listItemTrigger.ListTrigge.Count)], _listPoint[i].transform.position, quaternion.identity, transform).ItemTriggerSystem;
                _listItemTrigger.ListTrigge[i].Spawn(_listItem[Random.Range(0, _listItem.Count)].GetComponentInChildren<SpriteRenderer>());
                //_listItemTrigger.ListTrigge.Add(point.ItemTriggerSystem);
            }
        }
        
    }
}
