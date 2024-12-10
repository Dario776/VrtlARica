using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonPersistent<GameManager>
{
    private void Start()
    {

    }

    public void LoadGameScene()
    {
        SceneManager.UnloadSceneAsync("Start");
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
    }

    public void LoadStartScene()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadSceneAsync("Start", LoadSceneMode.Additive);
    }

    public void LoadEndScene()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadSceneAsync("End", LoadSceneMode.Additive);
    }


}
