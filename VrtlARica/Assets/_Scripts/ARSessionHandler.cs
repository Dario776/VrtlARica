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
            arSession.Reset(); // Reset session on start
            arSession.enabled = true; // Re-enable the AR session
        }
    }

    public void ClearTrackedImages()
    {
        Debug.Log("cistim images");
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
