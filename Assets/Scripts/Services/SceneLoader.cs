using UnityEngine.SceneManagement;

public class SceneLoader
{
    private const string MENU_SCENE = "Menu";
    private const string GAME_SCENE = "Game";

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMenuScene()
    {
        LoadScene(MENU_SCENE);
    }

    public void LoadGameScene()
    {
        LoadScene(GAME_SCENE);
    }
}