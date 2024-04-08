using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LuckyJet
{
    public class UIInstal : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<CoinUI>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<ScoreUI>()
                .FromComponentsInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<BestScoreUI>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<BuyBonusSystem>()
                .FromComponentsInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<RetryBtn>()
                .FromComponentsInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<HomeBtn>()
                .FromComponentsInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<DefeatUI>()
                .FromComponentsInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<BtnPushVolchek>()
                .FromComponentsInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<BonusBarUI>()
                .FromComponentsInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<SelectBonusUI>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<BonusUI>()
                .FromComponentsInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}
