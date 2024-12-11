using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInstantiate : MonoBehaviour
{
    [SerializeField]
    private Object persistentScene;

    public async void Load()
    {

        await SceneManager.LoadSceneAsync(persistentScene.name, LoadSceneMode.Additive);
    }
}
