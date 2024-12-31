using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class TapDetection : MonoBehaviour
{
    //potrebno da se detektiraju objekti koji se mogu selektirati, a ne npr. ARPlane
    [SerializeField]
    private LayerMask detectableLayer;
    [SerializeField]
    private bool isOutlined;
    private Outline outline;
    private static TapDetection selectedInstance;

    private void OnEnable()
    {
        Debug.Log("Tap detection enabled");
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += TapAction;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= TapAction;
    }

    private void TapAction(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) { return; }

        Ray ray = Camera.main.ScreenPointToRay(finger.currentTouch.screenPosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, detectableLayer))
        {
            GameObject tappedObject = hitInfo.collider.gameObject;
            TapDetection tapDetection = tappedObject.GetComponent<TapDetection>();

            if(tapDetection != null){
                tapDetection.ToggleOutline();
                Debug.Log("Tapped on object: " + tappedObject.name);
            }
            //ToggleOutline(tappedObject);
        }
    }

    private void ToggleOutline(GameObject tappedObject)
    {
        outline = tappedObject.GetComponent<Outline>();
        isOutlined = !isOutlined;
        outline.enabled = isOutlined;
    }

    //onemogucava selektiranje vise objekata odjednom
    private void ToggleOutline()
    {
        //Odznaci trenutno oznacen objekt ako postoji i ako nije trenutni objek
        if (selectedInstance != null && selectedInstance != this)
        {
            selectedInstance.SetOutline(false);
        }

        //Upravljanje outlineom za trenutni objekt
        isOutlined = !isOutlined;
        SetOutline(isOutlined);

        //Azuriranje selektiranog elementa
        if (isOutlined)
        {
            selectedInstance = this;
        }
        else if (selectedInstance == this)
        {
            selectedInstance = null;
        }
    }

    //postavljanje outlinea
    private void SetOutline(bool enable)
    {
        if (outline == null)
        {
            outline = GetComponent<Outline>();
        }

        if (outline != null)
        {
            outline.enabled = enable;
        }
    }

    public static TapDetection GetSelectedInstance(){
        return selectedInstance;
    }    
}
