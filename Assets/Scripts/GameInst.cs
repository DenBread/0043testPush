using System.Collections;
using System.Collections.Generic;
using MelenitasDev.SoundsGood;
using UnityEngine;
using Zenject;

namespace LuckyJet
{
    public class GameInst : MonoInstaller
    {
        private Music _musicMenu;
        public override void InstallBindings()
        {
            _musicMenu = new Music(Track.MusicMenu);
            _musicMenu
                .SetVolume(0.8f)
                .SetFadeOut(1f)
                .SetOutput(Output.Music)
                .SetLoop(true);
            _musicMenu.Play();
            
            Container
                .Bind<SpawerSystem>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<SelectLevelSystem>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<ModelSystem>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        
            Container
                .Bind<RotationSystem>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<ItemMoveSystem>()
                .FromComponentsInHierarchy()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<ListItemTrigger>()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<MagnetSystem>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            
         
        }
    }
}
