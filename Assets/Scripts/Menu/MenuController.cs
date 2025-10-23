using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private LoadScreenService _loadScreenService;

    private void Awake()
    {
        _startButton.onClick.AddListener(async () => await StartGame());
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public async Task StartGame()
    {
        await _loadScreenService.Show();
        await _sceneLoader.LoadSceneAsync(Scenes.Game);
    }
}
