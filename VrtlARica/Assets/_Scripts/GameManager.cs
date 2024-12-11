using System;
using UnityEngine;
using UnityEngine.SceneManagement;

enum Stanje
{
    Uvod,
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

    public override void Awake()
    {
        base.Awake();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //2 uvodna teksta
        zadovoljeniUvjeti = 2;
        stanje = Stanje.Uvod;
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool NextStep()
    {
        if (zadovoljeniUvjeti > 0)
        {
            zadovoljeniUvjeti--;
            if (zadovoljeniUvjeti == 0 && stanje == Stanje.Uvod)
            {
                stanje = Stanje.PostavljanjeZemlje;
                ObradiStanje();
            }
            else if (zadovoljeniUvjeti == 0)
                ObradiStanje();

            return true;
        }

        return false;
    }

    private void ObradiStanje()
    {
        switch (stanje)
        {
            case Stanje.PostavljanjeZemlje:
                Debug.Log(Stanje.PostavljanjeZemlje);
                PlaceObject placeObject = FindFirstObjectByType<PlaceObject>();
                placeObject.enabled = true;
                break;
            case Stanje.SkeniranjeMarkera:
                Debug.Log(Stanje.SkeniranjeMarkera);
                TrackedImageInfo trackedImageInfo = FindFirstObjectByType<TrackedImageInfo>();
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
        stanje = Stanje.SkeniranjeMarkera;
        zadovoljeniUvjeti++;
    }

    internal void SkeniranMarker()
    {
        stanje = Stanje.ZalijevanjeBiljke;
        zadovoljeniUvjeti++;
    }
}
