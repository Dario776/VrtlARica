using System.Collections;
using UnityEngine;

public class HarvestController : MonoBehaviour
{
    private TapDetection currentlyTapped;
    //prati koja je jabuka selektirana
    private TapDetection selectedApple;
    //prati selektiranu kosaru
    private TapDetection selectedBasket;
    //brojac koliko je jabuka ubrano s biljke i stavljeno u kosaru
    private int appleCounter = 0;

    private void Update()
    {
        //dohvati objekt na koji je trenutno korisnik kliknuo
        currentlyTapped = TapDetection.GetCurrentlyTappedObject();

        if (currentlyTapped != null)
        {
            if (currentlyTapped.name.Contains("red_apple"))
            {
                HandleAppleSelection(currentlyTapped);
            }

            if (currentlyTapped.name.Contains("basket"))
            {
                HandleBasketSelection(currentlyTapped);
            }
            //ako je selektirana kosara i jabuka
            if (selectedApple != null && selectedBasket != null)
            {   //potreban delay kako bi outline kosare i jabuke bio vidljiv
                StartCoroutine(Harvest(0.5f));
            }
        }
    }

    private void HandleAppleSelection(TapDetection tappedApple)
    {
        //onemogucuje selektiranje vise jabuka odjednom
        if (selectedApple != null && selectedApple != tappedApple)
        {
            //odznaci selektiranu jabuku
            selectedApple.ToggleOutline();
            selectedApple = null;
        }

        //oznaci/odznaci jabuku
        tappedApple.ToggleOutline();

        if (tappedApple.isOutlined)
        {
            selectedApple = tappedApple;
        }
    }

    private void HandleBasketSelection(TapDetection tappedBasket)
    {
        //oznaci/odznaci kosaru
        tappedBasket.ToggleOutline();

        if (tappedBasket.isOutlined)
        {
            selectedBasket = tappedBasket;
        }
        else
        {
            selectedBasket = null;
        }
    }

    private IEnumerator Harvest(float delay)
    {
        //bitno da se vidi outline jabuke i kosare
        yield return new WaitForSeconds(delay);
        //ukloni jabuku koja je selektirana
        DisableVisibility(selectedApple.gameObject);

        //odznaci kosaru
        selectedBasket.ToggleOutline();

        //dohvati jabuku koja je u hijerarhiji djete kosare i inicijalno je nevidljiva
        Transform appleChild = selectedBasket.transform.GetChild(appleCounter);
        //postavi vidljivost jabuke
        appleChild.gameObject.SetActive(true);
        //counter omogucava dohvat odredenog djeteta
        ++appleCounter;


        selectedBasket = null;

        //ukloni jabuku s biljke u hijerarhiji
        selectedApple.transform.SetParent(null);
        selectedApple = null;

        if (appleCounter == 10)
            GameManager.Instance.UbraniPlodovi();
    }

    private void DisableVisibility(GameObject obj)
    {
        if (obj == null)
            return;

        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        Debug.Log("Disabled visibility for object: " + obj.name);
    }

    public IEnumerator SkipHarvestInteraction()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log(GameManager.Instance.placeObject.trenutnaTeglica);
            Debug.Log(GameManager.Instance.placeObject.instantiatedBasket.transform.GetChild(i).gameObject);
            Debug.Log(GameManager.Instance.placeObject.trenutnaTeglica.transform.GetChild(i).gameObject);
            Debug.Log(GameManager.Instance.placeObject.instantiatedBasket.transform.GetChild(i).gameObject.name);
            Debug.Log(GameManager.Instance.placeObject.trenutnaTeglica.transform.GetChild(i).gameObject.name);
            GameManager.Instance.placeObject.instantiatedBasket.transform.GetChild(i).gameObject.SetActive(true);
            GameManager.Instance.placeObject.trenutnaTeglica.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}