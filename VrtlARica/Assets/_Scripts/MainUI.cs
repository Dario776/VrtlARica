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
    [SerializeField] private GameObject endButtons;
    [SerializeField] private GameObject endGestures;
    [SerializeField] private GameObject[] gameTextButtons;
    [SerializeField] private GameObject[] gameTextGestures;
    [SerializeField] private GameObject[] dots;
    private int i;
    private int isLeftArrowPressed;
    private GameManager gameManager;
    private AudioManager audioManager;
    private PostavkeManager postavkeManager;

    private void Awake()
    {
        i = 0;
        isLeftArrowPressed = 0;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        postavkeManager = PostavkeManager.Instance;
        //onemogucujemo lijevu strelicu na pocetku
        ToggleLeftArrow(false);
    }

    public void HomeButton()
    {
        audioManager.Play("startbutton");
        exit.SetActive(true);
    }

    public void CancelExit()
    {
        audioManager.Play("startbutton");
        exit.SetActive(false);
    }

    public void ConfirmExit()
    {
        audioManager.Play("startbutton");
        gameManager.LoadStartScene();
    }

    public void SettingsButton()
    {
        audioManager.Play("startbutton");
        settings.SetActive(true);
        dropdownMenu.SetActive(false);
    }

    public void HamburgerButton()
    {
        audioManager.Play("startbutton");
        if (!dropdownMenu.gameObject.activeSelf)
            dropdownMenu.SetActive(true);
        else
            dropdownMenu.SetActive(false);
    }

    public void RightArrowButton()
    {
        audioManager.Play("navigationbutton");

        if (isLeftArrowPressed > 0)
        {
            isLeftArrowPressed--;

            //iskljucujem trenutni text i tockicu (kod tockice ukljucujem bijeli krug unutar veceg smedeg)
            gameTextButtons[i].SetActive(false);
            gameTextGestures[i].SetActive(false);


            dots[i].SetActive(true);

            //micem se dalje
            i++;

            //ukljucujem sljedeci text i tockicu
            if (!postavkeManager.usingGestures)
            {
                gameTextButtons[i].SetActive(true);
            }
            else
            {
                gameTextGestures[i].SetActive(true);
            }
            dots[i].SetActive(false);

            if (isLeftArrowPressed == 0 && gameManager.conditions == 0)
            {
                ToggleRightArrow(false);
            }

            ToggleLeftArrow(true);
        }
        else if (gameManager.conditions > 0 && i < gameTextButtons.Length - 1)
        {

            gameTextButtons[i].SetActive(false);
            gameTextGestures[i].SetActive(false);


            dots[i].SetActive(true);

            i++;

            if (!postavkeManager.usingGestures)
            {
                gameTextButtons[i].SetActive(true);
            }
            else
            {
                gameTextGestures[i].SetActive(true);
            }

            dots[i].SetActive(false);

            gameManager.conditions--;

            if (gameManager.conditions == 0)
            {
                gameManager.HandleCurrentState();
                ToggleRightArrow(false);
            }

            ToggleLeftArrow(true);
        }
    }

    public void LeftArrowButton()
    {
        audioManager.Play("navigationbutton");
        //da nikad ne izade "out of bounds"
        if (i > 0)
        {
            isLeftArrowPressed++;

            gameTextButtons[i].SetActive(false);
            gameTextGestures[i].SetActive(false);

            dots[i].SetActive(true);

            //suprotan postupak od desne strelice
            i--;

            if (!postavkeManager.usingGestures)
            {
                gameTextButtons[i].SetActive(true);
            }
            else
            {
                gameTextGestures[i].SetActive(true);
            }

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
        audioManager.Play("movebutton");
        gameManager.placeObject.MoveCurrentObjectLeft();
    }

    public void MoveRightButton()
    {
        audioManager.Play("movebutton");
        gameManager.placeObject.MoveCurrentObjectRight();
    }

    public void MoveUpButton()
    {
        audioManager.Play("movebutton");
        gameManager.placeObject.MoveCurrentObjectUp();
    }

    public void MoveDownButton()
    {
        audioManager.Play("movebutton");
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
        audioManager.Play("rotatebutton");
        gameManager.rotateObject.RotateLeft();
    }

    public void RotateRightButton()
    {
        audioManager.Play("rotatebutton");
        gameManager.rotateObject.RotateRight();
    }

    public void ShowEndScreen()
    {
        audioManager.Play("endsuccess");
        end.SetActive(true);
        if (!postavkeManager.usingGestures)
        {
            endButtons.SetActive(true);
            endGestures.SetActive(false);
        }
        else
        {
            endButtons.SetActive(false);
            endGestures.SetActive(true);
        }

    }

    public void SkipInteraction()
    {
        audioManager.Play("startbutton");
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
