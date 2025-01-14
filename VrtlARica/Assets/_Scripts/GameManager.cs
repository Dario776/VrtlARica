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
    public int conditions;
    private bool skipSkip;
    private State state;
    private MainUI mainUI;
    public PlaceObject placeObject { get; private set; }
    public RotateObject rotateObject { get; private set; }
    private HarvestController harvestController;
    private PotCollision potCollision;
    private ImageTracker imageTracker;
    private Camera arCamera;

    private AudioManager audioManager;
    private PostavkeManager postavkeManager;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        audioManager = AudioManager.Instance;
        postavkeManager = PostavkeManager.Instance;
    }

    public void LoadGameScene()
    {
        //2 uvodna teksta, 2 puta dajemo prolaz
        conditions = 2;
        state = State.PotPlace;
        skipSkip = false;
        audioManager.Stop("mainmenumusic");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void HandleCurrentState()
    {
        StopAllCoroutines();
        skipSkip = false;
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
                if(!postavkeManager.usingGestures)
                    mainUI.ToggleMoveSeedButtons(true);
                else
                    placeObject.instantiatedSeed.GetComponent<DragAndDrop>().enabled = true;
                break;
            case State.WateringCanMove:
                Debug.Log(State.WateringCanMove);
                placeObject.CreateWateringCan();
                if(!postavkeManager.usingGestures)
                    mainUI.ToggleMoveSeedButtons(true);
                else
                    placeObject.instantiatedWateringCan.GetComponent<DragAndDrop>().enabled = true;
                break;
            case State.PlantGrow:
                Debug.Log(State.PlantGrow);
                if(!postavkeManager.usingGestures)
                    mainUI.TogglePlusButton(true);
                //geste omogucene u placeObjectu jer ima vise objekata u interakciji
                break;
            case State.FruitHarvest:
                Debug.Log(State.FruitHarvest);
                if(!postavkeManager.usingGestures){
                    mainUI.ToggleRotationButtons(true);
                    rotateObject.enabled = true;
                    rotateObject.SetRotationTarget(placeObject.currentPot);
                }
                else{
                    placeObject.currentPot.GetComponent<RotationGesture>().enabled = true;
                }
                placeObject.CreateBasket();
                harvestController.enabled = true;
                break;
            case State.PlantDecay:
                Debug.Log(State.PlantDecay);
                placeObject.ReplaceCurrentPotWithNextPotInLine();
                if(!postavkeManager.usingGestures)
                    mainUI.ToggleMinusButton(true);
                //geste omogucene u placeObjectu jer ima vise objekata u interakciji
                break;
            default:
                Debug.Log("Greska!");
                break;
        }
    }

    public void StartPotPlaced()
    {
        skipSkip = true;
        mainUI.ToggleSkipButton(false);

        state = State.MarkerScan;
        conditions++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");

        mainUI.RightArrowButton();   
    }

    public void MarkerScanned()
    {
        skipSkip = true;
        mainUI.ToggleSkipButton(false);
        imageTracker.enabled = false;

        state = State.SeedMove;
        conditions++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");

        mainUI.RightArrowButton();
    }

    public void SeedMoved()
    {
        skipSkip = true;
        mainUI.ToggleSkipButton(false);
        //onemoguci geste
        mainUI.ToggleMoveSeedButtons(false);
        placeObject.ReplaceCurrentPotWithNextPotInLine();

        state = State.WateringCanMove;
        conditions++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");

        mainUI.RightArrowButton();
    }

    public void WateringCanMoved()
    {
        skipSkip = true;
        mainUI.ToggleSkipButton(false);
        placeObject.ReplaceCurrentPotWithNextPotInLine();
        //onemoguci geste
        mainUI.ToggleMoveSeedButtons(false);

        state = State.PlantGrow;
        conditions++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");
    
        mainUI.RightArrowButton();
    }

    public void PlantGrew()
    {
        skipSkip = true;
        mainUI.ToggleSkipButton(false);
        //onemoguci geste
        mainUI.TogglePlusButton(false);

        state = State.FruitHarvest;
        conditions++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");

        mainUI.RightArrowButton();
    }

    public void FruitsHarvested()
    {
        skipSkip = true;
        mainUI.ToggleSkipButton(false);
        mainUI.ToggleRotationButtons(false);
        //onemoguci gestu
        placeObject.currentPot.GetComponent<RotationGesture>().enabled = false;
        placeObject.currentPot.GetComponent<RotationGesture>().DisableInputActions();

        state = State.PlantDecay;
        conditions++;
        mainUI.ToggleRightArrow(true);
        audioManager.Play("success");

        mainUI.RightArrowButton();
    }

    public void PlantDecayed()
    {
        skipSkip = true;
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

        if (!skipSkip)
        {
            mainUI.ToggleSkipButton(true);
            audioManager.Play("skipshow");
        }
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
