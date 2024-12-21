using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private GameObject targetObject;
    public float rotationDegree = 15f;
    public float rotationSpeed = 5f;

    private Quaternion targetRotation;
    void Update()
    {
        if(targetObject == null)
            return;
        
        targetObject.transform.rotation = Quaternion.Lerp(
            targetObject.transform.rotation,
            targetRotation,
            Time.deltaTime*rotationSpeed
        );
    }

    public void SetRotationTarget(GameObject target){
        targetObject = target;
        Debug.Log("targetObject=" + targetObject);
        targetRotation = targetObject.transform.rotation;
    }

    public void RotateLeft(){
        targetRotation *= Quaternion.Euler(0, -rotationDegree, 0);
    }

    public void RotateRight(){
        targetRotation *= Quaternion.Euler(0, rotationDegree, 0);
    }
}
