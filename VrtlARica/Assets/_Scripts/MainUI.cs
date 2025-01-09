using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject dropdownMenu;
    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button rightArrowButton;
    [SerializeField] private Image leftArrowImage;
    [SerializeField] private Image rightArrowImage;
    [SerializeField] private GameObject rotationButtons;
    [SerializeField] private GameObject moveButtons;
    [SerializeField] private GameObject plusButton;
    [SerializeField] private GameObject minusButton;
    [SerializeField] private GameObject skipButton;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject[] gameText;
    [SerializeField] private GameObject[] dots;
    private int i;
    private bool canToggle;
    private int isLeftArrowPressed;
    private GameManager gameManager;

    private void Awake()
    {
        i = 0;
        isLeftArrowPressed = 0;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        //onemogucujemo lijevu strelicu na pocetku
        ToggleLeftArrow(false);
    }

    public void HomeButton()
    {
        exit.SetActive(true);
    }

    public void CancelExit()
    {
        exit.SetActive(false);
    }

    public void ConfirmExit()
    {
        gameManager.LoadStartScene();
    }

    public void SettingsButton()
    {
        settings.SetActive(true);
        dropdownMenu.SetActive(false);
    }

    public void HamburgerButton()
    {
        if (!dropdownMenu.gameObject.activeSelf)
            dropdownMenu.SetActive(true);
        else
            dropdownMenu.SetActive(false);
    }

    public void RightArrowButton()
    {
        //provjera jel smo vratili tekst ulijevo pa samo zelimo doci opet do zadnjeg desnog teksta bez uplitavanja GameManagera
        if (isLeftArrowPressed > 0)
        {
            isLeftArrowPressed--;

            //iskljucujem trenutni text i tockicu (kod tockice ukljucujem bijeli krug unutar veceg smedeg)
            gameText[i].SetActive(false);
            dots[i].SetActive(true);

            //micem se dalje
            i++;

            //ukljucujem sljedeci text i tockicu
            gameText[i].SetActive(true);
            dots[i].SetActive(false);

            ToggleLeftArrow(true);

        }
        //provjera jel mogu ic dalje s igrom i namjestavanje desne strelice na temelju provjere
        else if (canToggle = gameManager.NextStep() && i < gameText.Length - 1)
        {
            ToggleRightArrow(canToggle);

            gameText[i].SetActive(false);
            dots[i].SetActive(true);

            i++;

            gameText[i].SetActive(true);
            dots[i].SetActive(false);

            ToggleLeftArrow(true);
        }
        else
        {
            ToggleRightArrow(canToggle);
        }
    }

    public void LeftArrowButton()
    {
        //da nikad ne izade "out of bounds"
        if (i > 0)
        {
            isLeftArrowPressed++;

            gameText[i].SetActive(false);
            dots[i].SetActive(true);

            //suprotan postupak od desne strelice
            i--;

            gameText[i].SetActive(true);
            dots[i].SetActive(false);

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
            leftArrowButton.interactable = true;
            leftArrowImage.color = Constants.CustomBrownColor;
        }
        else
        {
            leftArrowButton.interactable = false;
            leftArrowImage.color = Constants.CustomDisabledBrownColor;
        }
    }

    public void ToggleRightArrow(bool isEnabled)
    {
        if (isEnabled)
        {
            rightArrowButton.interactable = true;
            rightArrowImage.color = Constants.CustomBrownColor;
        }
        else
        {
            rightArrowButton.interactable = false;
            rightArrowImage.color = Constants.CustomDisabledBrownColor;
        }
    }

    public void ToggleRotationButtons(bool Enable)
    {
        if (Enable)
        {
            rotationButtons.SetActive(true);
        }
        else
        {
            rotationButtons.SetActive(false);
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
        gameManager.placeObject.MoveCurrentObjectLeft();
    }

    public void MoveRightButton()
    {
        gameManager.placeObject.MoveCurrentObjectRight();
    }

    public void MoveUpButton()
    {
        gameManager.placeObject.MoveCurrentObjectUp();
    }

    public void MoveDownButton()
    {
        gameManager.placeObject.MoveCurrentObjectDown();
    }

    public void MinusAndPlusButton()
    {
        gameManager.placeObject.ReplaceCurrentPotWithNextPotInLine();
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

    public void ShowEndScreen()
    {
        end.SetActive(true);
    }

    public void SkipInteraction()
    {
        gameManager.SkipInteraction();
    }

    public void ToggleSkipButton(bool Enable)
    {
        if (Enable)
        {
            skipButton.SetActive(true);
        }
        else
        {
            skipButton.SetActive(false);
        }
    }
}
