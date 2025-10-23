using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;

    [Inject] SceneLoader _sceneLoader;
    [Inject] LoadScreenService _loadScreenService;

    private void Start()
    {
        _homeButton.onClick.AddListener(async () => await OnHomeButtonClicked());
        _restartButton.onClick.AddListener(async () => await OnRestartButtonClicked());
    }

    private async Task OnHomeButtonClicked()
    {
        await _sceneLoader.LoadSceneAsync(Scenes.Menu);
    }

    private async Task OnRestartButtonClicked()
    {
        await _loadScreenService.Show();
        await _sceneLoader.ReloadCurrentScene();
    }
}