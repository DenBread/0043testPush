using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LuckyJet
{
    public class ProjectInst : MonoInstaller
    {
        public override void InstallBindings()
        {
            if(SaveLoadSystem.Load<VolchekDataSave>() == null)
                SaveLoadSystem.Save(new VolchekDataSave(0));
            if(SaveLoadSystem.Load<CoinDataSave>() == null)
                SaveLoadSystem.Save(new CoinDataSave());
            if(SaveLoadSystem.Load<BonusDataSave>() == null)
                SaveLoadSystem.Save(new BonusDataSave());
            if(SaveLoadSystem.Load<ScoreDataSave>() == null)
                SaveLoadSystem.Save(new ScoreDataSave());
            if(SaveLoadSystem.Load<SelectBonusDataSave>() == null)
                SaveLoadSystem.Save(new SelectBonusDataSave());
        }
    }
}
