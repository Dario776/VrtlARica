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

    void Awake()
    {
        aRPlaneManager = GetComponent<ARPlaneManager>();
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0)
            return;

        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            foreach (ARRaycastHit hit in hits)
            {
                Pose pose = hit.pose;
                GameObject obj = Instantiate(Zemlja, pose.position, pose.rotation);
                aRPlaneManager.enabled = false;
                ARPlaneMeshVisualizer[] aRPlaneMeshVisualizers = FindObjectsByType<ARPlaneMeshVisualizer>(FindObjectsSortMode.None);
                foreach (ARPlaneMeshVisualizer aRPlaneMesh in aRPlaneMeshVisualizers)
                {
                    aRPlaneMesh.enabled = false;
                }
                GameManager.Instance.ZemljaPostavljena();
                enabled = false;
            }
        }

    }
}
