using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    [SerializeField] GameObject Sjemenka;

    private List<GameObject> ARObjects = new List<GameObject>();
    private ARTrackedImageManager aRTrackedImageManager;

    private void Awake()
    {
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

    // Event Handler
    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        //Create object based on image tracked
        foreach (var trackedImage in eventArgs.added)
        {

            if (trackedImage.referenceImage.name == "crvenaJabuka" || trackedImage.referenceImage.name == "zelenaJabuka")
            {
                ListAllImages();
                var newPrefab = Instantiate(Sjemenka, trackedImage.transform);
                ARObjects.Add(newPrefab);

                //javljanje GameManageru da smo gotovi
                GameManager.Instance.SkeniranMarker();
            }
        }
    }

    //Update tracking position
    // foreach (var trackedImage in eventArgs.updated)
    // {

    //         if (gameObject.name == trackedImage.name)
    //         {
    //             gameObject.SetActive(trackedImage.trackingState == TrackingState.Tracking);
    //         }

    // }

    private void ListAllImages()
    {
        Debug.Log(
            $"There are {aRTrackedImageManager.trackables.count} images being tracked.");

        foreach (var trackedImage in aRTrackedImageManager.trackables)
        {
            Debug.Log($"Image: {trackedImage.referenceImage.name} is at " +
                      $"{trackedImage.transform.position}");
        }
    }
}