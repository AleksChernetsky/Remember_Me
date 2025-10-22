using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private const string MENU_SCENE = "Menu";
    private const string GAME_SCENE = "Game";

    public async Task LoadMenuScene()
    {
        await LoadSceneAsync(MENU_SCENE);
    }
    public async Task LoadGameScene()
    {
        await LoadSceneAsync(GAME_SCENE);
    }
    public async Task ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        await LoadSceneAsync(currentSceneName);
    }

    private async Task LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
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
    }
}