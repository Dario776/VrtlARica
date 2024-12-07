using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostavkeManager : MonoBehaviour
{
    static PostavkeManager _instance;
    public static PostavkeManager Instance { get { return _instance; } }

    public string currentFont;
    public int currentFontSize;

    public TMP_FontAsset sans;
    public TMP_FontAsset dyslexic;

    List<GameObject> texts;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            currentFont = "Sans";
            currentFontSize = 0;
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void CheckForText()
    {
        texts = new List<GameObject>();
        GameObject[] gameObjects = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (GameObject go in gameObjects)
        {
            if (go.tag == "Text")
                texts.Add(go);
        }
        CheckFont();
    }

    public void CheckFont()
    {
        if (currentFont == "Sans")
            ChangeFontSans();
        else
            ChangeFontDyslexic();
    }

    public void ChangeFontSans()
    {
        currentFont = "Sans";

        foreach (GameObject go in texts)
        {
            go.GetComponent<TextMeshProUGUI>().font = sans;
        }
    }
    public void ChangeFontDyslexic()
    {
        currentFont = "Dyslexic";

        foreach (GameObject go in texts)
        {
            go.GetComponent<TextMeshProUGUI>().font = dyslexic;
        }
    }
}
