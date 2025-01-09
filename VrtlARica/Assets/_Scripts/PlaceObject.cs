using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject[] teglicePrefab;
    [SerializeField] private GameObject basketPrefab;

    [SerializeField] public GameObject seedPrefab;
    [SerializeField] public GameObject wateringCanPrefab;

    public MoveObject currentObjectToMove;
    public GameObject instantiatedSeed;
    public GameObject instantiatedWateringCan;
    public GameObject instantiatedBasket;
    public GameObject trenutnaTeglica;
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //dodano da se zapamti pozicija gdje je stavljen prefab
    private bool objectPlaced;
    private int currentPotIndex;

    private void Awake()
    {
        objectPlaced = false;
        currentPotIndex = 0;
    }

    private void Start()
    {
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
            foreach (ARRaycastHit hit in hits)
            {
                // da nebi slucajno bilo dva objekta zemlje
                if (!objectPlaced)
                {
                    objectPlaced = true;
                    EnhancedTouch.Touch.onFingerDown -= FingerDown;

                    trenutnaTeglica = Instantiate(teglicePrefab[currentPotIndex], hit.pose.position, hit.pose.rotation);

                    Debug.Log("stvoren prefab teglice");
                    Debug.Log("Prefab Position: " + trenutnaTeglica.transform.position);

                    //iskljucivanje prepoznavanja ravnina i njihovih mesh-eva
                    aRPlaneManager.enabled = false;
                    ARPlaneMeshVisualizer[] aRPlaneMeshVisualizers = FindObjectsByType<ARPlaneMeshVisualizer>(FindObjectsSortMode.None);
                    foreach (ARPlaneMeshVisualizer aRPlaneMesh in aRPlaneMeshVisualizers)
                    {
                        aRPlaneMesh.enabled = false;
                    }

                    //javljanje GameManageru da smo gotovi
                    GameManager.Instance.ZemljaPostavljena();
                }
            }
        }
    }

    //dodano za zamijenu trenutnog modela
    public void ReplaceModel(GameObject toReplace, GameObject replaceWith)
    {
        if (toReplace != null)
        {
            Debug.Log("Uništenje " + toReplace);
            Destroy(toReplace);

            trenutnaTeglica = Instantiate(replaceWith, trenutnaTeglica.transform.position, trenutnaTeglica.transform.rotation);

            Debug.Log("Stvoren novi objekt " + toReplace);
        }
        else
        {
            Debug.LogWarning("Objekt ne postoji");
        }
    }

    public void ReplaceCurrentPotWithNextPotInLine()
    {
        currentPotIndex++;

        if (currentPotIndex == 5)
        {
            GameManager.Instance.BiljkaNarasla();
            Destroy(trenutnaTeglica);
            trenutnaTeglica = Instantiate(teglicePrefab[currentPotIndex], trenutnaTeglica.transform.position, trenutnaTeglica.transform.rotation);
        }
        else if (currentPotIndex == 10)
        {
            GameManager.Instance.BiljkaPropala();
        }
        else
        {
            Destroy(trenutnaTeglica);
            trenutnaTeglica = Instantiate(teglicePrefab[currentPotIndex], trenutnaTeglica.transform.position, trenutnaTeglica.transform.rotation);
        }
    }

    public void CreateBasket()
    {
        instantiatedBasket = Instantiate(basketPrefab, new Vector3(trenutnaTeglica.transform.position.x - 0.3f,
        trenutnaTeglica.transform.position.y, trenutnaTeglica.transform.position.z), Quaternion.identity);
    }

    public void CreateStartTeglica(Vector3 potPosition)
    {
        objectPlaced = true;

        trenutnaTeglica = Instantiate(teglicePrefab[currentPotIndex], potPosition, Quaternion.identity);

        Debug.Log("stvoren prefab teglice");
        Debug.Log("Prefab Position: " + trenutnaTeglica.transform.position);

        //iskljucivanje prepoznavanja ravnina i njihovih mesh-eva
        aRPlaneManager.enabled = false;
        ARPlaneMeshVisualizer[] aRPlaneMeshVisualizers = FindObjectsByType<ARPlaneMeshVisualizer>(FindObjectsSortMode.None);
        foreach (ARPlaneMeshVisualizer aRPlaneMesh in aRPlaneMeshVisualizers)
        {
            aRPlaneMesh.enabled = false;
        }
    }

    public void CreateWateringCan()
    {
        instantiatedWateringCan = Instantiate(wateringCanPrefab, new Vector3(trenutnaTeglica.transform.position.x + 0.5f,
        trenutnaTeglica.transform.position.y, trenutnaTeglica.transform.position.z), Quaternion.identity);
        currentObjectToMove = instantiatedWateringCan.GetComponent<MoveObject>();
    }

    public void CreateSeed()
    {
        instantiatedSeed = Instantiate(seedPrefab, new Vector3(trenutnaTeglica.transform.position.x + 0.5f,
        trenutnaTeglica.transform.position.y + 0.5f, trenutnaTeglica.transform.position.z), Quaternion.identity);
        currentObjectToMove = instantiatedSeed.GetComponent<MoveObject>();
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

    public IEnumerator SkipGrowInteraction()
    {
        int targetIndex;

        if (GameManager.Instance.stanje == Stanje.RastBiljke)
            targetIndex = 6;
        else
            targetIndex = 10;

        while (currentPotIndex < targetIndex)
        {
            yield return new WaitForSeconds(1);
            ReplaceCurrentPotWithNextPotInLine();
        }
    }
}
