using UnityEngine;


public class SizeUpObject : MonoBehaviour
{
    [SerializeField] private GameObject[] models; // Array to hold the models
    private PlaceObject placeObject;
    private int clickCount;
    private int currentIndex;

    public void Awake()
    {
        clickCount = 0;
        currentIndex = 0;
    }
    public void Start()
    {
        if (models == null || models.Length == 0)
        {
            Debug.LogError("No models assigned to the SizeUpObject script!");
            return;
        }
        placeObject = FindFirstObjectByType<PlaceObject>();
        placeObject.enabled = true;
    }

    public void OnButtonClick()
    {
        clickCount++;

        if (clickCount == 3)
        {
            if (currentIndex == 4)
                Debug.Log("dosli smo do kraja");
            else
            {
                if (currentIndex == 0)
                    placeObject.ReplaceModel(placeObject.GetTrenutnaTeglica(), models[currentIndex]);
                else
                    placeObject.ReplaceModel(models[currentIndex - 1], models[currentIndex]);

                currentIndex++;
            }

            clickCount = 0;
        }
    }

    public GameObject[] getModels()
    {
        return models;
    }
}
