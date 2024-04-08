using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuckyJet
{
    public static class SceneManager
    {
        public static void LoadScene(Scenes scene)
        {
            switch (scene)
            {
                case Scenes.Menu:
                    Screen.orientation = ScreenOrientation.Portrait;
                    break;
                case Scenes.Game:
                    Screen.orientation = ScreenOrientation.LandscapeLeft;
                    break;
                default:
                    Screen.orientation = ScreenOrientation.Portrait;
                    break;
            }
            
            
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
        }

        public static Scenes GetActiveScene()
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            Enum.TryParse(scene.name.ToString(), out Scenes scenes);
            return scenes;
        }

        public static bool CheckCurrentScene(Scenes scene)
        {
            return GetActiveScene() == scene;
        }
    }
    
    public enum Scenes
    {
        Menu,
        Game,
        Tutorial
    }
}
