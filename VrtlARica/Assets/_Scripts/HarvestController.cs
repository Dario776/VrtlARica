using UnityEngine;

public class HarvestController : MonoBehaviour
{
    [SerializeField]
    private GameObject GrownPlantWithFruit;

    [SerializeField]
    private GameObject Basket;

    void Update(){
        TapDetection selectedInstance = TapDetection.GetSelectedInstance();
        Debug.Log("selectedInstance in harvest controller = " + selectedInstance);
    }

    //geter za kosaru
    public GameObject GetBasket()
    {
        return Basket;
    }
    //getter za izraslu biljku
    public GameObject GetGrownPlantWithFruit()
    {
        return GrownPlantWithFruit;
    }
}
