using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.ARFoundation;

public class TrackedImageInfo : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            ListAllImages("ADD");
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            //stalno se updata
        }

        foreach (var removedImage in eventArgs.removed)
        {
            ListAllImages("REMOVE");
        }
    }
    void ListAllImages(string action)
    {
        Debug.Log(action);
        Debug.Log(
            $"There are {m_TrackedImageManager.trackables.count} images being tracked.");

        foreach (var trackedImage in m_TrackedImageManager.trackables)
        {
            Debug.Log($"Image: {trackedImage.referenceImage.name} is at " +
                      $"{trackedImage.transform.position}");
        }
    }
}
