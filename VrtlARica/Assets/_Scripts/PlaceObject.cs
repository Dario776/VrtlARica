using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlaceObject : MonoBehaviour
{

    [SerializeField]
    private GameObject Zemlja;

    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //dodano da se zapamti pozicija gdje je stavljen model
    private Pose pose;

    //dodano da se zapamti trenutni model
    private GameObject obj;

    private bool objectPlaced;

    private void Awake()
    {
        objectPlaced = false;
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

                    //Pose pose = hit.pose;
                    pose = hit.pose;
                    //GameObject obj = Instantiate(Zemlja, pose.position, pose.rotation);
                    obj = Instantiate(Zemlja, pose.position, pose.rotation);

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
    public void ReplaceModel(GameObject toReplace, GameObject replaceWith){
        if(toReplace != null)
        {
            Debug.Log("Uništenje " + toReplace);
            Destroy(toReplace);

            obj = Instantiate(replaceWith, pose.position, pose.rotation);

            Debug.Log("Stvoren novi objekt " + toReplace);
        } else {
            Debug.LogWarning("Objekt ne postoji");
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

    //getter za trenutni objekt
    public GameObject GetObject(){
        return obj;
    }

}
