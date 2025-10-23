using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Menu,
    Game
}

public class SceneLoader
{
    private Scenes _currentScene;

    public async Task LoadSceneAsync(Scenes sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName.ToString());
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            await Task.Yield();
        }

        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            await Task.Yield();
        }

        _currentScene = sceneName;
    }

    public async Task ReloadCurrentScene()
    {
        await LoadSceneAsync(_currentScene);
    }
}