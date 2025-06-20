using UnityEngine;

public class RotateObject : MonoBehaviour
{
    //referenca na objekt koji se rotira
    private GameObject targetObject;
    //za koliko stupnjeva se rotira
    public float rotationDegree = 15f;
    //kojom brzinom se rotira
    public float rotationSpeed = 5f;
    //potrebno da se rotacija izvede glatko
    private Quaternion targetRotation;

    void Update()
    {
        if (targetObject == null)
            return;

        //omogućava glatku rotaciju
        targetObject.transform.rotation = Quaternion.Lerp(
            targetObject.transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    //setter za objekt koji želimo rotirati
    public void SetRotationTarget(GameObject target)
    {
        targetObject = target;
        targetRotation = targetObject.transform.rotation;
    }

    //funkcije za gumbiće za rotaciju
    public void RotateLeft()
    {
        targetRotation *= Quaternion.Euler(0, -rotationDegree, 0);
    }

    public void RotateRight()
    {
        targetRotation *= Quaternion.Euler(0, rotationDegree, 0);
    }
}
