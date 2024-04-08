using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuckyJet
{
    public class MagnetSystem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ItemMoveSystem>(out ItemMoveSystem itemMoveSystem))
            {
                
                itemMoveSystem.OnMove?.Invoke(true, transform.localPosition);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ItemMoveSystem>(out ItemMoveSystem itemMoveSystem))
            {
                itemMoveSystem.OnMove?.Invoke(false, transform.position);
            }
        }
    }
}
