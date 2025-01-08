using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager aRTrackedImageManager;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        aRTrackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);
    }

    private void OnDisable()
    {
        aRTrackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
    }

    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            if (trackedImage.referenceImage.name == "crvenaJabuka" || trackedImage.referenceImage.name == "zelenaJabuka")
            {
                gameManager.placeObject.CreateSeed();
                gameManager.SkeniranMarker();
            }
        }
    }
}


