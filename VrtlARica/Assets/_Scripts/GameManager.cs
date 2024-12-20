using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

enum Stanje
{
    PostavljanjeZemlje,
    SkeniranjeMarkera,
    ZalijevanjeBiljke,
    RastBiljke,
    BerbaPlodova
}
public class GameManager : SingletonPersistent<GameManager>
{
    //semafor
    private int zadovoljeniUvjeti;
    private Stanje stanje;

    private MainUI mainUI;
    private PlaceObject placeObject;
    private TrackedImageInfo trackedImageInfo;


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
        switch (stanje)
        {
            case Stanje.PostavljanjeZemlje:
                Debug.Log(Stanje.PostavljanjeZemlje);
                placeObject = FindFirstObjectByType<PlaceObject>();
                placeObject.enabled = true;
                break;
            case Stanje.SkeniranjeMarkera:
                Debug.Log(Stanje.SkeniranjeMarkera);
                trackedImageInfo = FindFirstObjectByType<TrackedImageInfo>();
                trackedImageInfo.enabled = true;
                break;
            case Stanje.ZalijevanjeBiljke:
                Debug.Log(Stanje.ZalijevanjeBiljke);
                break;
            case Stanje.RastBiljke:
                Debug.Log(Stanje.RastBiljke);
                break;
            case Stanje.BerbaPlodova:
                Debug.Log(Stanje.BerbaPlodova);
                break;
            default:
                Debug.Log("Greska!");
                break;
        }
    }

    public void ZemljaPostavljena()
    {
        //onemogucavanje placeObjecta
        placeObject.enabled = false;

        //priprema za sljedece stanje 
        stanje = Stanje.SkeniranjeMarkera;
        zadovoljeniUvjeti++;
        mainUI = FindFirstObjectByType<MainUI>();
        mainUI.ToggleRightArrow(true);
    }

    public void SkeniranMarker()
    {
        trackedImageInfo.enabled = false;
        stanje = Stanje.ZalijevanjeBiljke;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }
}
