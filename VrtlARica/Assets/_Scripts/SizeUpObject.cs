using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class SizeUpObject : MonoBehaviour
{
    [SerializeField] private GameObject[] models; // Array to hold the models
    //public GameObject currentModel;
    public PlaceObject placeObject;
    private int clickCount = 0;

    public void Start()
    {
        if (models == null || models.Length == 0)
        {
            Debug.LogError("No models assigned to the SizeUpObject script!");
            return;
        }
        placeObject = FindFirstObjectByType<PlaceObject>();
                placeObject.enabled = true;

        // Ensure only the first model is active at the start
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(i == 0);
        }

        // Initialize currentModel
        //currentModel = models[0];
    }

    public void OnButtonClick()
    {
        if (models == null || models.Length == 0)
        {
            Debug.LogError("No models available for switching!");
            return;
        }

        clickCount++;
        int currentIndex = (clickCount - 1) % 3;
        int nextIndex = clickCount % 3;

        if (nextIndex == 3) {
            Debug.LogError("This is the last model!");
            return;
        }

        models[currentIndex].SetActive(false);
        models[nextIndex].SetActive(true);

        //currentModel = models[nextIndex];
        placeObject.ReplaceModel(models[currentIndex], models[nextIndex]);

        Debug.Log($"Switched to model: {models[nextIndex]}");
    }

    public GameObject[] getModels()
    {
        return models;
    }
}
