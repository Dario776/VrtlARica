using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

enum State
{
    PotPlace,
    MarkerScan,
    SeedMove,
    WateringCanMove,
    PlantGrow,
    FruitHarvest,
    PlantDecay
}

public class GameManager : SingletonPersistent<GameManager>
{
    private int conditionsSatisfied;
    private State state;
    private MainUI mainUI;
    public PlaceObject placeObject { get; private set; }
    public RotateObject rotateObject { get; private set; }
    private HarvestController harvestController;
    private PotCollision potCollision;
    private ImageTracker imageTracker;
    private Camera arCamera;

    private AudioManager audioManager;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        audioManager = AudioManager.Instance;
    }

    public void LoadGameScene()
    {
        //2 uvodna teksta, 2 puta dajemo prolaz
        conditionsSatisfied = 2;
        state = State.PotPlace;
        audioManager.Stop("mainmenumusic");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //ako je igrac napravio zadano, zadovoljeniUvjeti ce biti pozitivni i ova funkcija ce vratiti prolaz (true)
    public bool NextStep()
    {
        if (conditionsSatisfied > 0)
        {
            conditionsSatisfied--;
            //kad smo dosli do kraja prolaza, obradujemo stanje
            if (conditionsSatisfied == 0)
            {
                HandleCurrentState();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HandleCurrentState()
    {
        StartCoroutine(ShowSkipButton());

        switch (state)
        {
            case State.PotPlace:
                Debug.Log(State.PotPlace);
                FindComponents();
                placeObject.enabled = true;
                break;
            case State.MarkerScan:
                Debug.Log(State.MarkerScan);
                imageTracker.enabled = true;
                break;
            case State.SeedMove:
                Debug.Log(State.SeedMove);
                mainUI.ToggleMoveSeedButtons(true);
                break;
            case State.WateringCanMove:
                Debug.Log(State.WateringCanMove);
                break;
            case State.PlantGrow:
                Debug.Log(State.PlantGrow);
                mainUI.TogglePlusButton(true);
                break;
            case State.FruitHarvest:
                Debug.Log(State.FruitHarvest);
                mainUI.ToggleRotationButtons(true);
                placeObject.CreateBasket();
                rotateObject.enabled = true;
                harvestController.enabled = true;
                rotateObject.SetRotationTarget(placeObject.currentPot);
                break;
            case State.PlantDecay:
                Debug.Log(State.PlantDecay);
                mainUI.ToggleMinusButton(true);
                break;
            default:
                Debug.Log("Greska!");
                break;
        }
    }

    public void StartPotPlaced()
    {
        mainUI.ToggleSkipButton(false);

        state = State.MarkerScan;
        conditionsSatisfied++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");
    }

    public void MarkerScanned()
    {
        mainUI.ToggleSkipButton(false);
        imageTracker.enabled = false;

        state = State.SeedMove;
        conditionsSatisfied++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");
    }

    public void SeedMoved()
    {
        mainUI.ToggleSkipButton(false);
        placeObject.CreateWateringCan();
        placeObject.ReplaceCurrentPotWithNextPotInLine();

        state = State.WateringCanMove;
        conditionsSatisfied++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");
    }

    public void WateringCanMoved()
    {
        mainUI.ToggleSkipButton(false);
        placeObject.ReplaceCurrentPotWithNextPotInLine();
        mainUI.ToggleMoveSeedButtons(false);

        state = State.PlantGrow;
        conditionsSatisfied++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");
    }

    public void PlantGrew()
    {
        mainUI.ToggleSkipButton(false);
        mainUI.TogglePlusButton(false);

        state = State.FruitHarvest;
        conditionsSatisfied++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");
    }

    public void FruitsHarvested()
    {
        mainUI.ToggleSkipButton(false);
        mainUI.ToggleRotationButtons(false);

        state = State.PlantDecay;
        conditionsSatisfied++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");
    }

    public void PlantDecayed()
    {
        mainUI.ShowEndScreen();
    }

    public void SkipInteraction()
    {
        mainUI.ToggleSkipButton(false);

        switch (state)
        {
            case State.PotPlace:
                Vector3 position = arCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 3, 2f));
                Quaternion rotation = Quaternion.identity;
                Pose pose = new Pose(position, rotation);
                placeObject.CreateStartPot(pose);
                break;
            case State.MarkerScan:
                placeObject.CreateSeed();
                break;
            case State.SeedMove:
                potCollision = placeObject.currentPot.GetComponent<PotCollision>();
                potCollision.SeedMoved(placeObject.instantiatedSeed);
                break;
            case State.WateringCanMove:
                potCollision = placeObject.currentPot.GetComponent<PotCollision>();
                potCollision.WateringCanMoved(placeObject.instantiatedWateringCan);
                break;
            case State.PlantGrow:
                mainUI.TogglePlusButton(false);
                StartCoroutine(placeObject.SkipGrowInteraction());
                break;
            case State.FruitHarvest:
                mainUI.ToggleRotationButtons(false);
                StartCoroutine(harvestController.SkipHarvestInteraction());
                break;
            case State.PlantDecay:
                mainUI.ToggleMinusButton(false);
                StartCoroutine(placeObject.SkipRotInteraction());
                break;
            default:
                Debug.Log("Greska!");
                break;
        }
    }

    private IEnumerator ShowSkipButton()
    {
        yield return new WaitForSeconds(5f);
        audioManager.Play("skipshow");
        mainUI.ToggleSkipButton(true);
    }

    private void FindComponents()
    {
        mainUI = FindFirstObjectByType<MainUI>();
        placeObject = FindFirstObjectByType<PlaceObject>();
        imageTracker = FindFirstObjectByType<ImageTracker>();
        rotateObject = FindFirstObjectByType<RotateObject>();
        harvestController = FindFirstObjectByType<HarvestController>();
        arCamera = FindFirstObjectByType<Camera>();
    }
}
