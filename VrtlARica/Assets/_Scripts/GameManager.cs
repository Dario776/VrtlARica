using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

enum Stanje
{
    PostavljanjeZemlje,
    SkeniranjeMarkera,
    PomicanjeSjemenke,
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
    private GameObject XROriginGO;
    private PlaceObject placeObject;
    private ImageTracker imageTracker;

    [SerializeField] GameObject wateringCanPrefab;
    private GameObject transformedPot;
    public void SetTransformedPot(GameObject pot) { 
        transformedPot = pot; 
        Debug.Log("Transformed pot has been set."); 
        Debug.Log($"Transformed Pot Position: {transformedPot.transform.position}"); 
        Debug.Log($"Transformed Pot Rotation: {transformedPot.transform.rotation}"); 
        Debug.Log($"Transformed Pot Scale: {transformedPot.transform.localScale}"); 
    }

    private void SpawnWateringCan()
    {
        if (transformedPot != null && wateringCanPrefab != null)
        {
            Vector3 spawnPosition = transformedPot.transform.position + new Vector3(0, 0.5f, 0);
            Instantiate(wateringCanPrefab, spawnPosition, Quaternion.identity);
            Debug.LogWarning("Watering can spawned");
        }
        else
        {
            if (transformedPot == null) Debug.LogWarning("Transformed pot is not assigned."); 
            if (wateringCanPrefab == null) Debug.LogWarning("Watering can prefab is not assigned."); 
            Debug.LogWarning("nesto fali");
        }
    }

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
                PronalazakKomponenataIgre();
                placeObject.enabled = true;
                break;
            case Stanje.SkeniranjeMarkera:
                Debug.Log(Stanje.SkeniranjeMarkera);
                imageTracker.enabled = true;
                break;
            case Stanje.PomicanjeSjemenke:
                Debug.Log(Stanje.PomicanjeSjemenke);
                mainUI.ShowMoveButtons(true);
                break;
            case Stanje.ZalijevanjeBiljke:
                Debug.Log(Stanje.ZalijevanjeBiljke);
                mainUI.ShowMoveButtons(false);
                mainUI.ShowMoveButtons2(true);
                //SpawnWateringCan();
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

    private void PronalazakKomponenataIgre()
    {
        XROriginGO = FindFirstObjectByType<XROrigin>().gameObject;
        placeObject = XROriginGO.GetComponent<PlaceObject>();
        imageTracker = XROriginGO.GetComponent<ImageTracker>();
    }

    public void ZemljaPostavljena()
    {
        Debug.Log("zemlja postavljena called");
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
        Debug.Log("skeniran marker called");
        imageTracker.enabled = false;
        stanje = Stanje.PomicanjeSjemenke;  
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void SjemenkaPomaknuta()
    {
        Debug.Log("pomaknuta sjemneka called");
        stanje = Stanje.ZalijevanjeBiljke;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);
    }

    public void ZalivenaBiljka()
    {
        Debug.Log("zalivena biljka called");
        stanje = Stanje.RastBiljke;
        zadovoljeniUvjeti++;
        mainUI.ToggleRightArrow(true);

    }

}
