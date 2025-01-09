using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Stanje
{
    PostavljanjeZemlje,
    SkeniranjeMarkera,
    PomicanjeSjemenke,
    ZalijevanjeBiljke,
    RastBiljke,
    BerbaPlodova,
    PropadanjeBiljke
}
public class GameManager : SingletonPersistent<GameManager>
{
    private int zadovoljeniUvjeti;
    public Stanje stanje;
    private MainUI mainUI;
    public PlaceObject placeObject;
    public RotateObject rotateObject;
    private HarvestController harvestController;
    public ImageTracker imageTracker;
    private Camera camera;

    public override void Awake()
    {
        base.Awake();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //2 uvodna teksta, 2 puta dajemo prolaz
        zadovoljeniUvjeti = 2;
        stanje = Stanje.PostavljanjeZemlje;
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //ako je igrac napravio zadano, zadovoljeniUvjeti ce biti pozitivni i ova funkcija ce vratiti prolaz (true)
    public bool NextStep()
    {
        if (zadovoljeniUvjeti > 0)
        {
            zadovoljeniUvjeti--;
            Debug.Log("zadovoljeniUvjet= " + zadovoljeniUvjeti);
            //kad smo dosli do kraja prolaza, obradujemo stanje
            if (zadovoljeniUvjeti == 0)
            {
                ObradiStanje();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ObradiStanje()
    {
        StartCoroutine(ShowSkipButton());

        switch (stanje)
        {
            case Stanje.PostavljanjeZemlje:
                Debug.Log(Stanje.PostavljanjeZemlje);
                PronadiKomponente();
                placeObject.enabled = true;
                break;
            case Stanje.SkeniranjeMarkera:
                Debug.Log(Stanje.SkeniranjeMarkera);
                imageTracker.enabled = true;
                break;
            case Stanje.PomicanjeSjemenke:
                Debug.Log(Stanje.PomicanjeSjemenke);
                mainUI.ToggleMoveSeedButtons(true);
                break;
            case Stanje.ZalijevanjeBiljke:
                Debug.Log(Stanje.ZalijevanjeBiljke);
                break;
            case Stanje.RastBiljke:
                Debug.Log(Stanje.RastBiljke);
                mainUI.TogglePlusButton(true);
                break;
            case Stanje.BerbaPlodova:
                Debug.Log(Stanje.BerbaPlodova);
                mainUI.ToggleRotationButtons(true);
                placeObject.CreateBasket();
                rotateObject.enabled = true;
                harvestController.enabled = true;
                rotateObject.SetRotationTarget(placeObject.trenutnaTeglica);
                break;
            case Stanje.PropadanjeBiljke:
                Debug.Log(Stanje.PropadanjeBiljke);
                mainUI.ToggleMinusButton(true);
                break;
            default:
                Debug.Log("Greska!");
                break;
        }
    }

    private void PronadiKomponente()
    {
        mainUI = FindFirstObjectByType<MainUI>();
        placeObject = FindFirstObjectByType<PlaceObject>();
        imageTracker = FindFirstObjectByType<ImageTracker>();
        rotateObject = FindFirstObjectByType<RotateObject>();
        harvestController = FindFirstObjectByType<HarvestController>();
        camera = FindFirstObjectByType<Camera>();
    }

    public void ZemljaPostavljena()
    {
        mainUI.ToggleSkipButton(false);

        stanje = Stanje.SkeniranjeMarkera;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void SkeniranMarker()
    {
        mainUI.ToggleSkipButton(false);
        imageTracker.enabled = false;

        stanje = Stanje.PomicanjeSjemenke;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void SjemenkaPomaknuta()
    {
        mainUI.ToggleSkipButton(false);
        placeObject.CreateWateringCan();
        placeObject.ReplaceCurrentPotWithNextPotInLine();

        stanje = Stanje.ZalijevanjeBiljke;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void ZalivenaBiljka()
    {
        mainUI.ToggleSkipButton(false);
        placeObject.ReplaceCurrentPotWithNextPotInLine();

        mainUI.ToggleMoveSeedButtons(false);

        stanje = Stanje.RastBiljke;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void BiljkaNarasla()
    {
        mainUI.ToggleSkipButton(false);
        mainUI.TogglePlusButton(false);

        stanje = Stanje.BerbaPlodova;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void UbraniPlodovi()
    {
        mainUI.ToggleSkipButton(false);
        mainUI.ToggleRotationButtons(false);

        stanje = Stanje.PropadanjeBiljke;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void BiljkaPropala()
    {
        Debug.Log("GOTOVO!!!!!");
        mainUI.ShowEndScreen();
    }

    public void SkipInteraction()
    {
        mainUI.ToggleSkipButton(false);

        switch (stanje)
        {
            case Stanje.PostavljanjeZemlje:
                placeObject.CreateStartTeglica(camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 3, 2f)));
                ZemljaPostavljena();
                break;
            case Stanje.SkeniranjeMarkera:
                placeObject.CreateSeed();
                SkeniranMarker();
                break;
            case Stanje.PomicanjeSjemenke:
                Destroy(placeObject.instantiatedSeed);
                SjemenkaPomaknuta();
                break;
            case Stanje.ZalijevanjeBiljke:
                Destroy(placeObject.instantiatedWateringCan);
                ZalivenaBiljka();
                break;
            case Stanje.RastBiljke:
                mainUI.TogglePlusButton(false);
                StartCoroutine(placeObject.SkipGrowInteraction());
                break;
            case Stanje.BerbaPlodova:
                mainUI.ToggleRotationButtons(false);
                StartCoroutine(harvestController.SkipHarvestInteraction());
                UbraniPlodovi();
                break;
            case Stanje.PropadanjeBiljke:
                mainUI.ToggleMinusButton(false);
                StartCoroutine(placeObject.SkipGrowInteraction());
                break;
            default:
                Debug.Log("Greska!");
                break;
        }
    }

    private IEnumerator ShowSkipButton()
    {
        yield return new WaitForSeconds(0.5f);
        mainUI.ToggleSkipButton(true);
    }
}
