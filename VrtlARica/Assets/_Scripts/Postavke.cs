using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Postavke : MonoBehaviour
{
    public GameObject Naslovnica;
    public GameObject Button1;
    public GameObject Button2;
    private UnityEngine.UI.Image OpcijaFonta1;
    private UnityEngine.UI.Image OpcijaFonta2;
    public TMP_FontAsset sans;
    public TMP_FontAsset dyslexic;

    String currentFont;
    List<GameObject> texts;
    private void Awake()
    {
        texts = new List<GameObject>();
        currentFont = "Sans";
        GameObject[] gameObjects = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (GameObject go in gameObjects)
        {
            if (go.tag == "Text")
                texts.Add(go);
        }
    }

    private void Start()
    {
        OpcijaFonta1 = Button1.GetComponent<UnityEngine.UI.Image>();
        OpcijaFonta2 = Button2.GetComponent<UnityEngine.UI.Image>();
    }

    public void ShowNaslovnica()
    {
        Naslovnica.SetActive(true);
        this.gameObject.SetActive(false);
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
        OpcijaFonta1.color = new Color32(249, 194, 126, 255);
        OpcijaFonta2.color = new Color32(255, 255, 255, 255);

        foreach (GameObject go in texts)
        {
            go.GetComponent<TextMeshProUGUI>().font = sans;
        }
    }
    public void ChangeFontDyslexic()
    {
        currentFont = "Dyslexic";
        OpcijaFonta2.color = new Color32(249, 194, 126, 255);
        OpcijaFonta1.color = new Color32(255, 255, 255, 255);

        foreach (GameObject go in texts)
        {
            go.GetComponent<TextMeshProUGUI>().font = dyslexic;
        }
    }
}
