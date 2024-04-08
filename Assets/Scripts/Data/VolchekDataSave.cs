using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuckyJet
{
    public class VolchekDataSave : ISaveData
    {
        public List<int> IndexModel ;
        public List<int> IndexAppearance;
        public List<bool> BuyListVolchek;

        public VolchekDataSave(int _)
        {
        IndexModel = new List<int>(){0,0,0};
        IndexAppearance = new List<int>(){0,0,0};
        BuyListVolchek = new List<bool>(){true, false,false,false,false,false};
        }
    }
}
