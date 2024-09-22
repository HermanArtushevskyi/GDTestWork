using _Project.CodeBase.Runtime.Scenes.Interfaces;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Runtime.Scenes
{
    public class SceneLoader : ISceneLoader
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}