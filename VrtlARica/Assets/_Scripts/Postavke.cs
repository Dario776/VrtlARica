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

    private void Start()
    {
        OpcijaFonta1 = Button1.GetComponent<UnityEngine.UI.Image>();
        OpcijaFonta2 = Button2.GetComponent<UnityEngine.UI.Image>();
        CheckFont();
    }
    public void CheckFont()
    {
        if (PostavkeManager.Instance.currentFont == "Sans")
            ChangeFontSans();
        else
            ChangeFontDyslexic();
    }
    public void ShowNaslovnica()
    {
        Naslovnica.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void ChangeFontSans()
    {
        PostavkeManager.Instance.ChangeFontSans();
        OpcijaFonta1.color = new Color32(249, 194, 126, 255);
        OpcijaFonta2.color = new Color32(255, 255, 255, 255);
    }
    public void ChangeFontDyslexic()
    {
        PostavkeManager.Instance.ChangeFontDyslexic();
        OpcijaFonta2.color = new Color32(249, 194, 126, 255);
        OpcijaFonta1.color = new Color32(255, 255, 255, 255);
    }
    public void CloseThis()
    {
        this.gameObject.SetActive(false);
    }
}
