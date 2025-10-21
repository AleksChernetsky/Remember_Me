using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [Inject] private SceneLoader _sceneLoader;

    private void Awake()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        _sceneLoader.LoadGameScene();
    }
}
