using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject FontOption1;
    [SerializeField] private GameObject FontOption2;
    [SerializeField] private GameObject HomePageSoundOn;
    [SerializeField] private GameObject HomePageSoundOff;
    [SerializeField] private GameObject SoundOn;
    [SerializeField] private GameObject SoundOff;
    [SerializeField] private GameObject ButtonOption;
    [SerializeField] private GameObject GestureOption;

    private Image imageFontOption1;
    private Image imageFontOption2;
    private Image imageHomePageSoundOn;
    private Image imageHomePageSoundOff;
    private Image imageSoundOn;
    private Image imageSoundOff;
    private Image imageButtonOption;
    private Image imageGestureOption;

    private SettingsManager settingsManager;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        settingsManager = SettingsManager.Instance;

        imageFontOption1 = FontOption1.GetComponent<Image>();
        imageFontOption2 = FontOption2.GetComponent<Image>();

        imageHomePageSoundOn = HomePageSoundOn.GetComponent<Image>();
        imageHomePageSoundOff = HomePageSoundOff.GetComponent<Image>();

        imageSoundOn = SoundOn.GetComponent<Image>();
        imageSoundOff = SoundOff.GetComponent<Image>();

        imageButtonOption = ButtonOption.GetComponent<Image>();
        imageGestureOption = GestureOption.GetComponent<Image>();

        Refresh();
    }

    private void Refresh()
    {
        if (settingsManager.currentFont == "Sans")
        {
            imageFontOption1.color = Constants.CustomOrangeColor;
            imageFontOption2.color = Constants.WhiteColor;
        }
        else
        {
            imageFontOption1.color = Constants.WhiteColor;
            imageFontOption2.color = Constants.CustomOrangeColor;
        }

        imageHomePageSoundOn.color = audioManager.IsHomePageMuted ? Constants.WhiteColor : Constants.CustomOrangeColor;
        imageHomePageSoundOff.color = audioManager.IsHomePageMuted ? Constants.CustomOrangeColor : Constants.WhiteColor;

        imageSoundOn.color = audioManager.IsMuted ? Constants.WhiteColor : Constants.CustomOrangeColor;
        imageSoundOff.color = audioManager.IsMuted ? Constants.CustomOrangeColor : Constants.WhiteColor;

        imageButtonOption.color = settingsManager.usingGestures ? Constants.WhiteColor : Constants.CustomOrangeColor;
        imageGestureOption.color = settingsManager.usingGestures ? Constants.CustomOrangeColor : Constants.WhiteColor;
    }


    public void ChangeFontSansButton()
    {
        audioManager.Play("startbutton");
        settingsManager.ChangeFontSans();
        settingsManager.SaveSettings();
        imageFontOption1.color = Constants.CustomOrangeColor;
        imageFontOption2.color = Constants.WhiteColor;
    }
    public void ChangeFontDyslexicButton()
    {
        audioManager.Play("startbutton");
        settingsManager.ChangeFontDyslexic();
        settingsManager.SaveSettings();
        imageFontOption1.color = Constants.WhiteColor;
        imageFontOption2.color = Constants.CustomOrangeColor;
    }
    public void ChangeNSoundOnButton()
    {
        audioManager.SetHomePageMute(false);
        settingsManager.SaveSettings();
        audioManager.Play("startbutton");
        imageHomePageSoundOn.color = Constants.CustomOrangeColor;
        imageHomePageSoundOff.color = Constants.WhiteColor;

    }

    public void ChangeNSoundOffButton()
    {
        audioManager.Play("startbutton");
        audioManager.SetHomePageMute(true);
        settingsManager.SaveSettings();
        imageHomePageSoundOn.color = Constants.WhiteColor;
        imageHomePageSoundOff.color = Constants.CustomOrangeColor;
    }

    public void ChangeSoundOnButton()
    {
        audioManager.SetMute(false);
        audioManager.Play("startbutton");
        settingsManager.SaveSettings();
        imageSoundOn.color = Constants.CustomOrangeColor;
        imageSoundOff.color = Constants.WhiteColor;

    }

    public void ChangeSoundOffButton()
    {
        audioManager.Play("startbutton");
        audioManager.SetMute(true);
        settingsManager.SaveSettings();
        imageSoundOn.color = Constants.WhiteColor;
        imageSoundOff.color = Constants.CustomOrangeColor;
    }

    public void ChangeGumbiButton()
    {
        audioManager.Play("startbutton");
        settingsManager.usingGestures = false;
        settingsManager.SaveSettings();
        imageButtonOption.color = Constants.CustomOrangeColor;
        imageGestureOption.color = Constants.WhiteColor;
    }

    public void ChangeGesteButton()
    {
        audioManager.Play("startbutton");
        settingsManager.usingGestures = true;
        settingsManager.SaveSettings();
        imageGestureOption.color = Constants.CustomOrangeColor;
        imageButtonOption.color = Constants.WhiteColor;
    }

    public void CloseButton()
    {
        audioManager.Play("startbutton");
        gameObject.SetActive(false);
    }
}
