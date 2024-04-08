using UnityEngine;

namespace LuckyJet
{
    [CreateAssetMenu(fileName = "New Bonus", menuName = "Custom/Bonus", order = 51)]
    public class BonusProperties : ScriptableObject
    {
        public Sprite Sprite;
    }
}