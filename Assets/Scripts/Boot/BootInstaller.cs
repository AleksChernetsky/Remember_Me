using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BootInstaller : MonoBehaviour
{
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private LoadScreenService _loadScreenService;

    private async void Start()
    {
        await _loadScreenService.Show();
        await _sceneLoader.LoadMenuScene();
        await Task.Delay(1000);
        await _loadScreenService.Hide();
    }
}