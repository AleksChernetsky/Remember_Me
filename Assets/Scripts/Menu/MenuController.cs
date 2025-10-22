using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private LoadScreenService _loadScreenService;

    private void Awake()
    {
        _startButton.onClick.AddListener(async () => await StartGame());
    }

    public async Task StartGame()
    {
        await _loadScreenService.Show();
        await _sceneLoader.LoadGameScene();
    }
}
