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
    private GameObject trenutnaTeglica;
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //dodano da se zapamti pozicija gdje je stavljen prefab
    private Pose poseTrenutnaTeglica;
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

                    poseTrenutnaTeglica = hit.pose;

                    trenutnaTeglica = Instantiate(teglicePrefab[currentPotIndex], poseTrenutnaTeglica.position, poseTrenutnaTeglica.rotation);

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

            trenutnaTeglica = Instantiate(replaceWith, poseTrenutnaTeglica.position, poseTrenutnaTeglica.rotation);

            Debug.Log("Stvoren novi objekt " + toReplace);
        }
        else
        {
            Debug.LogWarning("Objekt ne postoji");
        }
    }

    public void ReplaceCurrentPotWithNextPotInLine()
    {
        if (currentPotIndex >= 4)
        {
            GameManager.Instance.BiljkaNarasla();
        }
        Destroy(trenutnaTeglica);
        currentPotIndex++;
        trenutnaTeglica = Instantiate(teglicePrefab[currentPotIndex], poseTrenutnaTeglica.position, poseTrenutnaTeglica.rotation);
    }

    public void ReplaceCurrentPotWithLastPotInLine()
    {
        if (currentPotIndex >= 3)
        {
            Destroy(trenutnaTeglica);
            currentPotIndex--;
            trenutnaTeglica = Instantiate(teglicePrefab[currentPotIndex], poseTrenutnaTeglica.position, poseTrenutnaTeglica.rotation);
        }
    }

    //ovo znaci da ce objekt biti 0.3 m udaljen od drugog objekta
    private float spawnDistance = 0.3f;
    //funkcija za omogućavanje prikaza objekta pored drugog objekta
    public void SpawnObject(GameObject objectToSpawn, GameObject refObject)
    {
        Debug.Log("objectToSpawn=" + objectToSpawn);
        Debug.Log("refObject=" + refObject);
        //izračun pozicije za stvaranje objekta
        Vector3 spawnPosition = refObject.transform.position + refObject.transform.right.normalized * spawnDistance;

        spawnPosition.y = refObject.transform.position.y;

        Instantiate(objectToSpawn, spawnPosition, refObject.transform.rotation);

    }

    //getter za trenutnu teglicu
    public GameObject GetTrenutnaTeglica()
    {
        return trenutnaTeglica;
    }

}
