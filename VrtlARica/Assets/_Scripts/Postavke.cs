using System;
using UnityEngine;
using UnityEngine.UI;

public class Postavke : MonoBehaviour
{
    [SerializeField] private GameObject OpcijaFonta1;
    [SerializeField] private GameObject OpcijaFonta2;
    [SerializeField] private GameObject OpcijaZvukUkljucenN;
    [SerializeField] private GameObject OpcijaZvukIskljucenN;
    [SerializeField] private GameObject OpcijaZvukUkljucen;
    [SerializeField] private GameObject OpcijaZvukIskljucen;
    [SerializeField] private GameObject OpcijaGumbi;
    [SerializeField] private GameObject OpcijaGeste;

    private Image imageOpcijaFonta1;
    private Image imageOpcijaFonta2;
    private Image imageZvukUkljucenN;
    private Image imageZvukIskljucenN;
    private Image imageZvukUkljucen;
    private Image imageZvukIskljucen;
    private Image imageOpcijaGumbi;
    private Image imageOpcijaGeste;

    private PostavkeManager postavkeManager;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        postavkeManager = PostavkeManager.Instance;

        imageOpcijaFonta1 = OpcijaFonta1.GetComponent<Image>();
        imageOpcijaFonta2 = OpcijaFonta2.GetComponent<Image>();

        imageZvukUkljucenN = OpcijaZvukUkljucenN.GetComponent<Image>();
        imageZvukIskljucenN = OpcijaZvukIskljucenN.GetComponent<Image>();

        imageZvukUkljucen = OpcijaZvukUkljucen.GetComponent<Image>();
        imageZvukIskljucen = OpcijaZvukIskljucen.GetComponent<Image>();

        imageOpcijaGumbi = OpcijaGumbi.GetComponent<Image>();
        imageOpcijaGeste = OpcijaGeste.GetComponent<Image>();

        Refresh();
    }

    private void Refresh()
    {
        if (postavkeManager.currentFont == "Sans")
        {
            imageOpcijaFonta1.color = Constants.CustomOrangeColor;
            imageOpcijaFonta2.color = Constants.WhiteColor;
        }
        else
        {
            imageOpcijaFonta1.color = Constants.WhiteColor;
            imageOpcijaFonta2.color = Constants.CustomOrangeColor;
        }

        imageZvukUkljucenN.color = audioManager.IsNaslovnicaMuted ? Constants.WhiteColor : Constants.CustomOrangeColor;
        imageZvukIskljucenN.color = audioManager.IsNaslovnicaMuted ? Constants.CustomOrangeColor : Constants.WhiteColor;

        imageZvukUkljucen.color = audioManager.IsMuted ? Constants.WhiteColor : Constants.CustomOrangeColor;
        imageZvukIskljucen.color = audioManager.IsMuted ? Constants.CustomOrangeColor : Constants.WhiteColor;

        imageOpcijaGumbi.color = postavkeManager.usingGestures ? Constants.WhiteColor : Constants.CustomOrangeColor;
        imageOpcijaGeste.color = postavkeManager.usingGestures ? Constants.CustomOrangeColor : Constants.WhiteColor;
    }


    public void ChangeFontSansButton()
    {
        audioManager.Play("startbutton");
        postavkeManager.ChangeFontSans();
        postavkeManager.SaveSettings();
        imageOpcijaFonta1.color = Constants.CustomOrangeColor;
        imageOpcijaFonta2.color = Constants.WhiteColor;
    }
    public void ChangeFontDyslexicButton()
    {
        audioManager.Play("startbutton");
        postavkeManager.ChangeFontDyslexic();
        postavkeManager.SaveSettings();
        imageOpcijaFonta1.color = Constants.WhiteColor;
        imageOpcijaFonta2.color = Constants.CustomOrangeColor;
    }
    public void ChangeNSoundOnButton()
    {
        audioManager.SetNaslovnicaMute(false);
        postavkeManager.SaveSettings();
        audioManager.Play("startbutton");
        imageZvukUkljucenN.color = Constants.CustomOrangeColor;
        imageZvukIskljucenN.color = Constants.WhiteColor;

    }

    public void ChangeNSoundOffButton()
    {
        audioManager.Play("startbutton");
        audioManager.SetNaslovnicaMute(true);
        postavkeManager.SaveSettings();
        imageZvukUkljucenN.color = Constants.WhiteColor;
        imageZvukIskljucenN.color = Constants.CustomOrangeColor;
    }

    public void ChangeSoundOnButton()
    {
        audioManager.SetMute(false);
        audioManager.Play("startbutton");
        postavkeManager.SaveSettings();
        imageZvukUkljucen.color = Constants.CustomOrangeColor;
        imageZvukIskljucen.color = Constants.WhiteColor;

    }

    public void ChangeSoundOffButton()
    {
        audioManager.Play("startbutton");
        audioManager.SetMute(true);
        postavkeManager.SaveSettings();
        imageZvukUkljucen.color = Constants.WhiteColor;
        imageZvukIskljucen.color = Constants.CustomOrangeColor;
    }

    public void ChangeGumbiButton()
    {
        audioManager.Play("startbutton");
        postavkeManager.usingGestures = false;
        postavkeManager.SaveSettings();
        imageOpcijaGumbi.color = Constants.CustomOrangeColor;
        imageOpcijaGeste.color = Constants.WhiteColor;
    }

    public void ChangeGesteButton()
    {
        audioManager.Play("startbutton");
        postavkeManager.usingGestures = true;
        postavkeManager.SaveSettings();
        imageOpcijaGeste.color = Constants.CustomOrangeColor;
        imageOpcijaGumbi.color = Constants.WhiteColor;
    }

    public void CloseButton()
    {
        audioManager.Play("startbutton");
        gameObject.SetActive(false);
    }
}
