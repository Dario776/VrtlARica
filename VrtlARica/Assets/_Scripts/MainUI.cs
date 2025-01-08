using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject Hint;
    [SerializeField] private GameObject Postavke;
    [SerializeField] private GameObject IzlazPopup;
    [SerializeField] private GameObject DropdownMenu;
    [SerializeField] private GameObject Zamrzni;
    [SerializeField] private GameObject StrelicaUlijevoObjekt;
    [SerializeField] private GameObject StrelicaUdesnoObjekt;
    [SerializeField] private GameObject StrelicaUlijevoVizualno;
    [SerializeField] private GameObject StrelicaUdesnoVizualno;
    [SerializeField] private GameObject StreliceZaRotaciju;
    [SerializeField] private GameObject moveButtons;
    [SerializeField] private GameObject plusButton;
    [SerializeField] private GameObject minusButton;
    [SerializeField] private GameObject[] Text;
    [SerializeField] private GameObject[] Tockice;

    private int i;
    private bool canToggle;
    private int pritisnutaStrelicaUlijevo;
    private Button strelicaUlijevoButton;
    private Button strelicaUdesnoButton;
    private Image strelicaUlijevoVizualnoImage;
    private Image strelicaUdesnoVizualnoImage;
    private MoveObject moveObject;

    private GameManager gameManager;

    private void Awake()
    {
        i = 0;
        pritisnutaStrelicaUlijevo = 0;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        strelicaUlijevoButton = StrelicaUlijevoObjekt.GetComponent<Button>();
        strelicaUdesnoButton = StrelicaUdesnoObjekt.GetComponent<Button>();

        strelicaUlijevoVizualnoImage = StrelicaUlijevoVizualno.GetComponent<Image>();
        strelicaUdesnoVizualnoImage = StrelicaUdesnoVizualno.GetComponent<Image>();

        moveObject = moveButtons.GetComponent<MoveObject>();

        //onemogucujemo lijevu strelicu na pocetku
        ToggleLeftArrow(false);
    }

    public void HomeButton()
    {
        IzlazPopup.SetActive(true);
    }

    public void CancelExit()
    {
        IzlazPopup.SetActive(false);
    }

    public void ConfirmExit()
    {
        gameManager.LoadStartScene();
    }

    public void HintButton()
    {
        Debug.Log("Treba dodati Hint funkcionalost.");
    }

    public void PostavkeButton()
    {
        Postavke.SetActive(true);
        DropdownMenu.SetActive(false);
    }

    public void HamburgerButton()
    {
        if (!DropdownMenu.gameObject.activeSelf)
            DropdownMenu.SetActive(true);
        else
            DropdownMenu.SetActive(false);
    }


    public void ZamrzniButton()
    {
        Debug.Log("Treba dodati Zamrzni funkcionalost.");

        // NE RADI, TREBA POPRAVAK
        // NE RADI, TREBA POPRAVAK

        if (Time.timeScale > 0)
        {
            Zamrzni.GetComponent<Image>().color = Konstante.CustomBlueColor;
            Time.timeScale = 0;
        }
        else
        {
            Zamrzni.GetComponent<Image>().color = Konstante.WhiteColor;
            Time.timeScale = 1;
        }
    }

    public void StrelicaUdesnoButton()
    {
        //provjera jel smo vratili tekst ulijevo pa samo zelimo doci opet do zadnjeg desnog teksta bez uplitavanja GameManagera
        if (pritisnutaStrelicaUlijevo > 0)
        {
            pritisnutaStrelicaUlijevo--;

            //iskljucujem trenutni text i tockicu (kod tockice ukljucujem bijeli krug unutar veceg smedeg)
            Text[i].SetActive(false);
            Tockice[i].SetActive(true);

            //micem se dalje
            i++;

            //ukljucujem sljedeci text i tockicu
            Text[i].SetActive(true);
            Tockice[i].SetActive(false);

            ToggleLeftArrow(true);

        }
        //provjera jel mogu ic dalje s igrom i namjestavanje desne strelice na temelju provjere
        else if (canToggle = gameManager.NextStep() && i < Text.Length - 1)
        {
            ToggleRightArrow(canToggle);

            Text[i].SetActive(false);
            Tockice[i].SetActive(true);

            i++;

            Text[i].SetActive(true);
            Tockice[i].SetActive(false);

            ToggleLeftArrow(true);
        }
        else
        {
            ToggleRightArrow(canToggle);
        }
    }

    public void StrelicaUlijevoButton()
    {
        //da nikad ne izade "out of bounds"
        if (i > 0)
        {
            pritisnutaStrelicaUlijevo++;

            Text[i].SetActive(false);
            Tockice[i].SetActive(true);

            //suprotan postupak od desne strelice
            i--;

            Text[i].SetActive(true);
            Tockice[i].SetActive(false);

            if (i == 0)
            {
                ToggleLeftArrow(false);
            }

            ToggleRightArrow(true);
        }
    }

    private void ToggleLeftArrow(bool isEnabled)
    {
        if (isEnabled)
        {
            strelicaUlijevoButton.interactable = true;
            strelicaUlijevoVizualnoImage.color = Konstante.CustomBrownColor;
        }
        else
        {
            strelicaUlijevoButton.interactable = false;
            strelicaUlijevoVizualnoImage.color = Konstante.CustomDisabledBrownColor;
        }
    }

    //mora bit public da ju GameManager moze aktivirat
    public void ToggleRightArrow(bool isEnabled)
    {
        if (isEnabled)
        {
            strelicaUdesnoButton.interactable = true;
            strelicaUdesnoVizualnoImage.color = Konstante.CustomBrownColor;
        }
        else
        {
            strelicaUdesnoButton.interactable = false;
            strelicaUdesnoVizualnoImage.color = Konstante.CustomDisabledBrownColor;
        }
    }

    public void ToggleRotationButtons(bool Enable)
    {
        if (Enable)
        {
            StreliceZaRotaciju.SetActive(true);
        }
        else
        {
            StreliceZaRotaciju.SetActive(false);
        }
    }

    public void ToggleMoveSeedButtons(bool Enable)
    {
        if (Enable)
        {
            moveButtons.SetActive(true);
        }
        else
        {
            moveButtons.SetActive(false);
        }
    }

    public void MoveLeftButton()
    {
        gameManager.imageTracker.MoveCurrentObjectLeft();
    }

    public void MoveRightButton()
    {
        gameManager.imageTracker.MoveCurrentObjectRight();
    }

    public void MoveUpButton()
    {
        gameManager.imageTracker.MoveCurrentObjectUp();
    }

    public void MoveDownButton()
    {
        gameManager.imageTracker.MoveCurrentObjectDown();
    }

    public void SizeUpButton()
    {
        gameManager.placeObject.ReplaceCurrentPotWithNextPotInLine();
    }

    public void SizeDownButton()
    {
        gameManager.placeObject.ReplaceCurrentPotWithLastPotInLine();
    }

    public void TogglePlusButton(bool Enable)
    {
        if (Enable)
        {
            plusButton.SetActive(true);
        }
        else
        {
            plusButton.SetActive(false);
        }
    }

    public void ToggleMinusButton(bool Enable)
    {
        if (Enable)
        {
            minusButton.SetActive(true);
        }
        else
        {
            minusButton.SetActive(false);
        }
    }

    public void RotateLeftButton()
    {
        gameManager.rotateObject.RotateLeft();
    }

    public void RotateRightButton()
    {
        gameManager.rotateObject.RotateRight();
    }
}
