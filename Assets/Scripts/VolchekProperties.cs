using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuckyJet
{
    [CreateAssetMenu(fileName = "New Volchek", menuName = "Custom/Volchek", order = 51)]
    public class VolchekProperties : ScriptableObject
    {
        public string TxtInfo;
        public Sprite ModelVolchek;
        public DifficultyVolchek DifficultyVolchek;
        public List<Sprite> ListMap;

        public List<Sprite> ListVolchek;
    }

    public enum DifficultyVolchek
    {
        Easy, Average, Hard
    }
}
