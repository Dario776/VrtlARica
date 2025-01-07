using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    [SerializeField] GameObject Sjemenka; // Prefab to instantiate
    private List<GameObject> ARObjects = new List<GameObject>();
    private ARTrackedImageManager aRTrackedImageManager;
    private GameObject instantiatedSeed; // Store reference to the instantiated seed

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

    // Event Handler for when tracked images change
    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            if (trackedImage.referenceImage.name == "crvenaJabuka" || trackedImage.referenceImage.name == "zelenaJabuka")
            {
                var newPrefab = Instantiate(Sjemenka, trackedImage.transform);
                instantiatedSeed = newPrefab;  // Store the reference of the instantiated seed
                Debug.Log("stvoren prefab");
                Debug.Log("Prefab Position: " + newPrefab.transform.position);

                MoveObject moveObjectScript = newPrefab.GetComponent<MoveObject>();
                if (moveObjectScript == null)
                {
                    moveObjectScript = newPrefab.AddComponent<MoveObject>(); // Add the MoveObject script if missing
                    Debug.Log("move object dodan");
                }

                moveObjectScript.SetMoveTarget(newPrefab);  // Pass the instantiated prefab to MoveObject script

                // Optionally notify GameManager that the marker was scanned
                GameManager.Instance.SkeniranMarker();
            }
        }
    }

    // Method to move the instantiated seed left
    public void MoveSeedLeft()
    {
        Debug.Log("pozvan moveseedleft");
        if (instantiatedSeed != null)
        {
            Debug.Log("pozvan instantseed");
            var moveObjectScript = instantiatedSeed.GetComponent<MoveObject>();
            if (moveObjectScript != null)
            {
                Debug.Log("movan");
                moveObjectScript.MoveLeft();
            }
        }
    }

    // Method to move the instantiated seed right
    public void MoveSeedRight()
    {
        if (instantiatedSeed != null)
        {
            var moveObjectScript = instantiatedSeed.GetComponent<MoveObject>();
            if (moveObjectScript != null)
            {
                moveObjectScript.MoveRight();
            }
        }
    }

    // Method to move the instantiated seed up
    public void MoveSeedUp()
    {
        if (instantiatedSeed != null)
        {
            var moveObjectScript = instantiatedSeed.GetComponent<MoveObject>();
            if (moveObjectScript != null)
            {
                moveObjectScript.MoveUp();
            }
        }
    }

    // Method to move the instantiated seed down
    public void MoveSeedDown()
    {
        if (instantiatedSeed != null)
        {
            var moveObjectScript = instantiatedSeed.GetComponent<MoveObject>();
            if (moveObjectScript != null)
            {
                moveObjectScript.MoveDown();
            }
        }
    }

    // Update tracking position (optional, depending on the use case)
    private void ListAllImages()
    {
        Debug.Log($"There are {aRTrackedImageManager.trackables.count} images being tracked.");

        foreach (var trackedImage in aRTrackedImageManager.trackables)
        {
            Debug.Log($"Image: {trackedImage.referenceImage.name} is at {trackedImage.transform.position}");
        }
    }

    // Utility method for recursive layer assignment (if needed)
    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}
