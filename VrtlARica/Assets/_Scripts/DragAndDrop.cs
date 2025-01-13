using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField]
    private InputAction press, screenPosition;

    private Vector3 currentScreenPosition;
    private bool dragged;

    // ravnina paralelna kameri (na temelju pozicije trenutnog objekta)
    private float objectZPosition;

    private void Awake()
    {
        screenPosition.Enable();
        press.Enable();

        // inicijalizacija trenutne pozicije zaslona
        screenPosition.performed += OnScreenPositionPerformed;

        //postavljanje z koordinate objekta
        objectZPosition = transform.position.z;
    }

    private void Update()
    {
        // Provjeri je li prst pritisnut na zaslon i je li pritisnut na objektu
        if (press.ReadValue<float>() > 0f)
        {
            if (TappedOn && !dragged)
            {
                StartCoroutine(Drag());
            }
        }
        else
        {
            // zaustavi pomicanje kad prst nije pritisnut
            dragged = false;
        }

        // azuriraj poziciju zaslona
        currentScreenPosition = screenPosition.ReadValue<Vector2>();
    }


    private void OnScreenPositionPerformed(InputAction.CallbackContext context)
    {
        currentScreenPosition = context.ReadValue<Vector2>();
    }

    private bool TappedOn
    {
        get
        {
            Ray ray = Camera.main.ScreenPointToRay(currentScreenPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.transform == transform;
            }
            return false;
        }
    }

    //ogranicenje na ravninu paralelnu s kamerom
    private Vector3 WorldPosition
    {
        get
        {
            float z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 screenPosWithZ = new Vector3(currentScreenPosition.x, currentScreenPosition.y, z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosWithZ);

            worldPos.z = objectZPosition;

            return worldPos;
        }
    }

    private IEnumerator Drag()
    {
        dragged = true;

        Vector3 offset = transform.position - WorldPosition;

        while (dragged)
        {
            // azuriraj poziciju
            transform.position = WorldPosition + offset;
            yield return null;
        }
    }
}