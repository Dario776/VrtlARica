using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInstantiate : MonoBehaviour
{
    [SerializeField]
    private Object persistentScene;
    private static bool done = false;
    private async void Awake()
    {
        if (!done)
        {
            await SceneManager.LoadSceneAsync(persistentScene.name, LoadSceneMode.Additive);
            done = true;
        }
    }
}
