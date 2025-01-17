using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSessionHandler : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;

    private void Start()
    {
        ARSession arSession = GetComponent<ARSession>();
        if (arSession != null)
        {
            arSession.Reset(); 
            arSession.enabled = true; 
        }
    }

    private void ClearTrackedImages()
    {
        if (trackedImageManager != null)
        {
            foreach (var trackedImage in trackedImageManager.trackables)
            {
                Destroy(trackedImage.gameObject);
            }
        }
    }

    private void OnDisable()
    {
        ClearTrackedImages();
    }
}
