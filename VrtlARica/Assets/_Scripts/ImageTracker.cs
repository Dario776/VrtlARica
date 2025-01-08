using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTracker : MonoBehaviour
{
    [SerializeField] GameObject seedPrefab;
    [SerializeField] GameObject wateringCanPrefab;

    private MoveObject currentObjectToMove;
    private GameObject instantiatedSeed;
    private GameObject instantiatedWateringCan;

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

    // Event Handler for when tracked images change
    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            if (trackedImage.referenceImage.name == "crvenaJabuka" || trackedImage.referenceImage.name == "zelenaJabuka")
            {
                ListAllImages();
                // var newPrefab = Instantiate(Sjemenka, trackedImage.transform);
                instantiatedSeed = Instantiate(seedPrefab, new Vector3(GameManager.Instance.placeObject.GetTrenutnaTeglica().transform.position.x + 0.5f,
                GameManager.Instance.placeObject.GetTrenutnaTeglica().transform.position.y + 0.5f, GameManager.Instance.placeObject.GetTrenutnaTeglica().transform.position.z), Quaternion.identity);

                currentObjectToMove = instantiatedSeed.GetComponent<MoveObject>();

                Debug.Log("stvoren prefab");
                Debug.Log("Prefab Position: " + instantiatedSeed.transform.position);

                GameManager.Instance.SkeniranMarker();
            }
        }
    }

    private void ListAllImages()
    {
        Debug.Log($"There are {aRTrackedImageManager.trackables.count} images being tracked.");

        foreach (var trackedImage in aRTrackedImageManager.trackables)
        {
            Debug.Log($"Image: {trackedImage.referenceImage.name} is at {trackedImage.transform.position}");
        }
    }

    public void MoveCurrentObjectLeft()
    {
        currentObjectToMove.MoveLeft();
    }

    public void MoveCurrentObjectRight()
    {
        currentObjectToMove.MoveRight();
    }

    public void MoveCurrentObjectUp()
    {
        currentObjectToMove.MoveUp();
    }

    public void MoveCurrentObjectDown()
    {
        currentObjectToMove.MoveDown();
    }

    public void CreateWateringCan()
    {
        instantiatedWateringCan = Instantiate(wateringCanPrefab, new Vector3(GameManager.Instance.placeObject.GetTrenutnaTeglica().transform.position.x + 0.5f,
        GameManager.Instance.placeObject.GetTrenutnaTeglica().transform.position.y + 0.5f, GameManager.Instance.placeObject.GetTrenutnaTeglica().transform.position.z), Quaternion.identity);
        currentObjectToMove = instantiatedWateringCan.GetComponent<MoveObject>();
    }
}


