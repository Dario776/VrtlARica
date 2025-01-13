using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostavkeManager : SingletonPersistent<PostavkeManager>
{
    public string currentFont { get; set; }
    public int currentFontSize { get; set; }
    public bool usingGestures { get; set; }

    private TMP_FontAsset sans;
    private TMP_FontAsset dyslexic;

    private TextMeshProUGUI[] texts;

    public override void Awake()
    {
        base.Awake();
        currentFont = PlayerPrefs.GetString("CurrentFont", "Sans");
        currentFontSize = PlayerPrefs.GetInt("CurrentFontSize", 0);
        usingGestures= PlayerPrefs.GetInt("UsingGestures", 0) == 1;
        sans = Resources.Load<TMP_FontAsset>(Constants.OpenSansRegularPath);
        dyslexic = Resources.Load<TMP_FontAsset>(Constants.OpenDyslexicRegularPath);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetString("CurrentFont", currentFont);
        PlayerPrefs.SetInt("IsMuted", AudioManager.Instance.IsMuted ? 1 : 0);
        PlayerPrefs.SetInt("IsGestureMode", usingGestures ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Settings saved!");
    }

    private void Start()
    {
        Refresh();
        RefreshSound();
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
        RefreshSound();
    }

    private void Refresh()
    {
        texts = FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        if (currentFont == "Sans")
        {
            ChangeFontSans();
        }
        else
        {
            ChangeFontDyslexic();
        }
    }

    private void RefreshSound()
    {
        AudioManager.Instance.SetMute(AudioManager.Instance.IsMuted);
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
