using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject[] potPrefab;
    [SerializeField] private GameObject basketPrefab;
    [SerializeField] private GameObject seedPrefab;
    [SerializeField] private GameObject wateringCanPrefab;
    public MoveObject currentObjectToMove { get; private set; }
    public GameObject instantiatedSeed { get; private set; }
    public GameObject instantiatedWateringCan { get; private set; }
    public GameObject instantiatedBasket { get; private set; }
    public GameObject currentPot { get; private set; }
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public int currentPotIndex;
    private bool isPotPlaced;
    private GameManager gameManager;
    private AudioManager audioManager;
    private PostavkeManager postavkeManager;

    private void Awake()
    {
        currentPotIndex = 0;
        isPotPlaced = false;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        postavkeManager = PostavkeManager.Instance;
        aRPlaneManager = GetComponent<ARPlaneManager>();
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0)
            return;

        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            if (!isPotPlaced)
                CreateStartPot(hits.First().pose);
        }
    }

    public void CreateStartPot(Pose potPose)
    {
        isPotPlaced = true;
        EnhancedTouch.Touch.onFingerDown -= FingerDown; // iskljucuje FingerDown funkciju

        currentPot = Instantiate(potPrefab[currentPotIndex], potPose.position, potPose.rotation);

        //iskljucivanje prepoznavanja ravnina i njihovih mesh-eva
        aRPlaneManager.enabled = false;
        ARPlaneMeshVisualizer[] aRPlaneMeshVisualizers = FindObjectsByType<ARPlaneMeshVisualizer>(FindObjectsSortMode.None);
        foreach (ARPlaneMeshVisualizer aRPlaneMesh in aRPlaneMeshVisualizers)
        {
            aRPlaneMesh.enabled = false;
        }

        //javljanje GameManageru da smo gotovi
        gameManager.StartPotPlaced();
    }

    public void CreateSeed()
    {
        instantiatedSeed = Instantiate(seedPrefab, new Vector3(currentPot.transform.position.x + 0.5f,
        currentPot.transform.position.y + 0.5f, currentPot.transform.position.z), Quaternion.identity);
        currentObjectToMove = instantiatedSeed.GetComponent<MoveObject>();
        gameManager.MarkerScanned();
    }

    public void CreateWateringCan()
    {
        instantiatedWateringCan = Instantiate(wateringCanPrefab, new Vector3(currentPot.transform.position.x + 0.5f,
        currentPot.transform.position.y, currentPot.transform.position.z), Quaternion.identity);
        currentObjectToMove = instantiatedWateringCan.GetComponent<MoveObject>();
    }

    public void CreateBasket()
    {
        instantiatedBasket = Instantiate(basketPrefab, new Vector3(currentPot.transform.position.x - 0.3f,
        currentPot.transform.position.y, currentPot.transform.position.z), Quaternion.identity);
    }

    public void ReplaceCurrentPotWithNextPotInLine()
    {
        currentPotIndex++;
        audioManager.Play("movebutton");

        PinchDetection pinchDetectionDisable = currentPot.GetComponent<PinchDetection>();
        if (postavkeManager.usingGestures && pinchDetectionDisable != null)
        {
            pinchDetectionDisable.enabled = false;
        }

        if (currentPotIndex == 6)
        {
            Destroy(currentPot);
            currentPot = Instantiate(potPrefab[currentPotIndex], currentPot.transform.position, currentPot.transform.rotation);
            gameManager.PlantGrew();
        }
        else if (currentPotIndex == 11)
        {
            Destroy(currentPot);
            currentPot = Instantiate(potPrefab[0], currentPot.transform.position, currentPot.transform.rotation);
            gameManager.PlantDecayed();
        }
        else
        {
            Destroy(currentPot);
            currentPot = Instantiate(potPrefab[currentPotIndex], currentPot.transform.position, currentPot.transform.rotation);

            PinchDetection pinchDetection = currentPot.GetComponent<PinchDetection>();
            if (postavkeManager.usingGestures && pinchDetection != null)
            {
                pinchDetection.enabled = true;
            }
        }
    }

    public IEnumerator SkipGrowInteraction()
    {
        while (currentPotIndex < 6)
        {
            yield return new WaitForSeconds(0.25f);
            ReplaceCurrentPotWithNextPotInLine();
        }
    }

    public IEnumerator SkipRotInteraction()
    {
        while (currentPotIndex < 11)
        {
            yield return new WaitForSeconds(0.25f);
            ReplaceCurrentPotWithNextPotInLine();
        }
    }

    public void MoveCurrentObjectLeft()
    {
        currentObjectToMove.MoveLeft();
    }

    public void MoveCurrentObjectRight()
    {
        currentObjectToMove.MoveRight();
    }

    public void MoveCurrentObjectUp()
    {
        currentObjectToMove.MoveUp();
    }

    public void MoveCurrentObjectDown()
    {
        currentObjectToMove.MoveDown();
    }
}