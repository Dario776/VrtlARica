using System.Collections;
using UnityEngine;

public class HarvestController : MonoBehaviour
{
    private OutlineOnTap currentlyTapped;
    //prati koja je jabuka selektirana
    private OutlineOnTap selectedApple;
    //prati selektiranu kosaru
    private OutlineOnTap selectedBasket;
    //brojac koliko je jabuka ubrano s biljke i stavljeno u kosaru
    private int appleCounter = 0;
    private bool isFruitHarvested;

    private bool isHarvesting;
    private bool isProcessingAppleTap;
    private bool isProcessingBasketTap;

    private GameManager gameManager;
    private AudioManager audioManager;

    private void Awake()
    {
        isFruitHarvested = false;
        isHarvesting = false;
        isProcessingAppleTap = false;
        isProcessingBasketTap = false;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        //dohvati objekt na koji je trenutno korisnik kliknuo
        currentlyTapped = OutlineOnTap.GetCurrentlyTappedObject();

        if (currentlyTapped != null)
        {
            if (currentlyTapped.name.Contains(Constants.redApple))
            {
                HandleAppleSelection(currentlyTapped);
            }

            if (currentlyTapped.name.Contains(Constants.basket))
            {
                HandleBasketSelection(currentlyTapped);
            }
            //ako je selektirana kosara i jabuka
            if (selectedApple != null && selectedBasket != null && !isHarvesting)
            {   //potreban delay kako bi outline kosare i jabuke bio vidljiv
                StartCoroutine(Harvest(0.5f));
            }
        }
    }

    private void HandleAppleSelection(OutlineOnTap tappedApple)
    {
        if (isHarvesting || isProcessingAppleTap) return;

        StartCoroutine(HandleAppleSelectionWithDelay(tappedApple));
    }

    private IEnumerator HandleAppleSelectionWithDelay(OutlineOnTap tappedApple)
    {
        isProcessingAppleTap = true;
        audioManager.Play("tapdetect");

        if (selectedApple != null && selectedApple != tappedApple)
        {
            selectedApple.ToggleOutline();
            selectedApple = null;
        }

        tappedApple.ToggleOutline();

        if (tappedApple.isOutlined)
        {
            selectedApple = tappedApple;
        }

        yield return new WaitForSeconds(0.2f);
        isProcessingAppleTap = false;
    }

    private void HandleBasketSelection(OutlineOnTap tappedBasket)
    {
        if (isHarvesting || isProcessingBasketTap) return;

        StartCoroutine(HandleBasketSelectionWithDelay(tappedBasket));
    }

    private IEnumerator HandleBasketSelectionWithDelay(OutlineOnTap tappedBasket)
    {
        isProcessingBasketTap = true;
        audioManager.Play("tapdetect");

        tappedBasket.ToggleOutline();

        if (tappedBasket.isOutlined)
        {
            selectedBasket = tappedBasket;
        }
        else
        {
            selectedBasket = null;
        }

        yield return new WaitForSeconds(0.2f);
        isProcessingBasketTap = false;
    }
    private IEnumerator Harvest(float delay)
    {
        isHarvesting = true;
        //bitno da se vidi outline jabuke i kosare
        yield return new WaitForSeconds(delay);

        //ukloni jabuku koja je selektirana
        selectedApple.gameObject.SetActive(false);
        selectedApple.gameObject.GetComponent<OutlineOnTap>().enabled = false;

        //odznaci kosaru
        selectedBasket.ToggleOutline();

        //dohvati jabuku koja je u hijerarhiji djete kosare i inicijalno je nevidljiva
        //postavi vidljivost jabuke
        selectedBasket.transform.GetChild(appleCounter).gameObject.SetActive(true);

        //counter omogucava dohvat odredenog djeteta
        ++appleCounter;
        selectedBasket = null;
        selectedApple = null;

        if (appleCounter == 10 && !isFruitHarvested)
        {
            isFruitHarvested = true;
            gameManager.FruitsHarvested();
        }

        isHarvesting = false;
    }

    public IEnumerator SkipHarvestInteraction()
    {
        if (!isFruitHarvested)
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(0.25f);
                audioManager.Play("tapdetect");
                gameManager.placeObject.instantiatedBasket.transform.GetChild(i).gameObject.SetActive(true);
                gameManager.placeObject.currentPot.transform.GetChild(i).gameObject.SetActive(false);
            }
            gameManager.FruitsHarvested();
        }
    }
}