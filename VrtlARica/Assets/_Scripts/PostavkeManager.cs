using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostavkeManager : SingletonPersistent<PostavkeManager>
{
    public string currentFont { get; set; }
    public int currentFontSize { get; set; }

    private TMP_FontAsset sans;
    private TMP_FontAsset dyslexic;

    private TextMeshProUGUI[] texts;

    public override void Awake()
    {
        base.Awake();
        currentFont = "Sans";
        currentFontSize = 0;
        sans = Resources.Load<TMP_FontAsset>(Konstante.OpenSansRegularPath);
        dyslexic = Resources.Load<TMP_FontAsset>(Konstante.OpenDyslexicRegularPath);
    }

    private void Start()
    {
        Refresh();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneChanged;
    }

    private void SceneChanged(Scene scene, LoadSceneMode mode)
    {
        Refresh();
    }

    private void Refresh()
    {
        texts = FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        if (currentFont == "Sans")
            ChangeFontSans();
        else
            ChangeFontDyslexic();
    }

    public void ChangeFontSans()
    {
        currentFont = "Sans";

        foreach (TextMeshProUGUI text in texts)
        {
            text.font = sans;
        }
    }

    public void ChangeFontDyslexic()
    {
        currentFont = "Dyslexic";

        foreach (TextMeshProUGUI text in texts)
        {
            text.font = dyslexic;
        }
    }
}
