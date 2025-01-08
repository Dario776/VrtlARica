using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTracker : MonoBehaviour
{
    [SerializeField] GameObject Sjemenka; 
    [SerializeField] GameObject transformedPotModel; //privremeneo dok pot collision ne radi
    [SerializeField] GameObject transformedPotModel2; //privremeno
    [SerializeField] GameObject waterCanModel; // privremeno dok collision ne radi
    private List<GameObject> ARObjects = new List<GameObject>();
    private ARTrackedImageManager aRTrackedImageManager;
    private GameObject instantiatedSeed; 
    private GameObject pot;
    private GameObject instantiatedWateringCan;
    private GameObject instantiatedTransformedPot;

    private void Awake()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    public void SetPot(GameObject pot)
    {
        this.pot = pot;
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
                var newPrefab = Instantiate(Sjemenka, new Vector3(pot.transform.position.x + 1f, pot.transform.position.y + 1f, pot.transform.position.z), Quaternion.identity);
                instantiatedSeed = newPrefab;  
                Debug.Log("stvoren prefab");
                Debug.Log("Prefab Position: " + newPrefab.transform.position);

                MoveObject moveObjectScript = newPrefab.GetComponent<MoveObject>();
                if (moveObjectScript == null)
                {
                    moveObjectScript = newPrefab.AddComponent<MoveObject>(); 
                    Debug.Log("move object dodan");
                }

                moveObjectScript.SetMoveTarget(newPrefab); 

                GameManager.Instance.SkeniranMarker();
            }
        }
    }
    public void MoveSeedLeft()
    {
        if (instantiatedSeed != null)
        {
            var moveObjectScript = instantiatedSeed.GetComponent<MoveObject>();
            if (moveObjectScript != null)
            {
                moveObjectScript.MoveLeft();

            }
        }
    }

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

    public void MoveSeedUp()
    {
        Debug.Log("up");
        if (instantiatedSeed != null)
        {
            var moveObjectScript = instantiatedSeed.GetComponent<MoveObject>();
            if (moveObjectScript != null)
            {
                moveObjectScript.MoveUp();

            }
        }
    }

    public void MoveSeedDown()
    {
        Debug.Log("down");
        if (instantiatedSeed != null)
        {
            var moveObjectScript = instantiatedSeed.GetComponent<MoveObject>();
            if (moveObjectScript != null)
            {
                moveObjectScript.MoveDown();
                Debug.Log("zovi destroy");
                TransformPotAndDestroySeed();
                GameManager.Instance.SjemenkaPomaknuta();
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

    private void TransformPotAndDestroySeed() { 
        if (pot != null && transformedPotModel != null) {
            GameObject newTransformedPot = Instantiate(transformedPotModel, pot.transform.position, pot.transform.rotation);
            newTransformedPot.transform.localScale = pot.transform.localScale;
            GameObject newWaterCanModel = Instantiate(waterCanModel, pot.transform.position + new Vector3(0.0f, 1f, 0.0f), pot.transform.rotation);
            instantiatedWateringCan = newWaterCanModel;
            instantiatedTransformedPot = newTransformedPot;

            MoveObject moveObjectScript = newWaterCanModel.GetComponent<MoveObject>();
            if (moveObjectScript == null)
            {
                moveObjectScript = newWaterCanModel.AddComponent<MoveObject>();
                Debug.Log("move object dodan az vodu");
            }

            moveObjectScript.SetMoveTarget(newWaterCanModel);


            GameManager.Instance.SetTransformedPot(newTransformedPot);
            Destroy(pot);
            Debug.Log("Pot has been transformed."); 
        } else { 
            Debug.LogWarning("Pot or transformed pot model is not assigned."); 
        } 
        if (instantiatedSeed != null) { 
            Destroy(instantiatedSeed); 
            Debug.Log("Seed has been destroyed."); 
        } else { 
            Debug.LogWarning("Seed instance is not available."); 
        } 
    }
    public void MoveWateringCanLeft() { 
        if (instantiatedWateringCan != null) { 
            var moveObjectScript = instantiatedWateringCan.GetComponent<MoveObject>();
            if (moveObjectScript != null) { moveObjectScript.MoveLeft(); } 
        } 
    }
    public void MoveWateringCanRight() { 
        if (instantiatedWateringCan != null) { 
            var moveObjectScript = instantiatedWateringCan.GetComponent<MoveObject>();
            if (moveObjectScript != null) {
                moveObjectScript.MoveRight(); 
            } 
        }
    }
    public void MoveWateringCanUp() { 
        if (instantiatedWateringCan != null) { 
            var moveObjectScript = instantiatedWateringCan.GetComponent<MoveObject>(); 
            if (moveObjectScript != null) { moveObjectScript.MoveUp(); }
        } 
    }
    public void MoveWateringCanDown() { 
        if (instantiatedWateringCan != null) { 
            var moveObjectScript = instantiatedWateringCan.GetComponent<MoveObject>(); 
            if (moveObjectScript != null) { 
                moveObjectScript.MoveDown();
                TransformPotAndDestroyWaterCan();
                GameManager.Instance.SjemenkaPomaknuta();
            } 
        } 
    }

    private void TransformPotAndDestroyWaterCan()
    {
        if (instantiatedWateringCan != null && transformedPotModel2 != null)
        {
            
            GameObject newTransformedPot2 = Instantiate(transformedPotModel2, instantiatedTransformedPot.transform.position, instantiatedTransformedPot.transform.rotation);
            newTransformedPot2.transform.localScale = instantiatedTransformedPot.transform.localScale;

           
            GameManager.Instance.SetTransformedPot(newTransformedPot2);

           
            Destroy(instantiatedTransformedPot);
            Debug.Log("Pot has been upgraded to stage 2.");

         
            Destroy(instantiatedWateringCan);
            Debug.Log("Watering can has been destroyed.");
        }
        else
        {
            Debug.LogWarning("Watering can or transformed pot model 2 is not assigned.");
        }
    }

}


