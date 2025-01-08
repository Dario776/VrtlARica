using UnityEngine;
using UnityEngine.SceneManagement;

enum Stanje
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
    private Stanje stanje;
    private MainUI mainUI;
    public PlaceObject placeObject;
    public RotateObject rotateObject;
    private HarvestController harvestController;
    private SizeUpObject sizeUpObject;
    public ImageTracker imageTracker;

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
                rotateObject.SetRotationTarget(placeObject.GetTrenutnaTeglica());
                break;
            case Stanje.PropadanjeBiljke:
                Debug.Log(Stanje.PropadanjeBiljke);

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
        sizeUpObject = FindFirstObjectByType<SizeUpObject>();
        rotateObject = FindFirstObjectByType<RotateObject>();
    }

    public void ZemljaPostavljena()
    {
        //onemogucavanje placeObjecta
        placeObject.enabled = false;

        //priprema za sljedece stanje 
        stanje = Stanje.SkeniranjeMarkera;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void SkeniranMarker()
    {
        stanje = Stanje.PomicanjeSjemenke;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void SjemenkaPomaknuta()
    {
        imageTracker.CreateWateringCan();
        placeObject.ReplaceCurrentPotWithNextPotInLine();

        stanje = Stanje.ZalijevanjeBiljke;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void ZalivenaBiljka()
    {
        placeObject.ReplaceCurrentPotWithNextPotInLine();

        imageTracker.enabled = false;
        mainUI.ToggleMoveSeedButtons(false);

        stanje = Stanje.RastBiljke;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void BiljkaNarasla()
    {
        mainUI.TogglePlusButton(false);

        stanje = Stanje.BerbaPlodova;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void UbraniPlodovi()
    {
        mainUI.ToggleRotationButtons(false);

        stanje = Stanje.PropadanjeBiljke;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }
}
