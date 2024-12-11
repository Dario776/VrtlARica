using UnityEngine;

public class Naslovnica : MonoBehaviour
{
    [SerializeField]
    private GameObject Upute;
    [SerializeField]
    private GameObject Info;
    [SerializeField]
    private GameObject Postavke;
    [SerializeField]
    private GameObject IzlazPopup;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void ZapocniButton()
    {
        gameManager.LoadGameScene();
    }

    public void IzlazButton()
    {
        IzlazPopup.SetActive(true);
    }

    public void CancelExit()
    {
        IzlazPopup.SetActive(false);
    }

    public void ConfirmExit()
    {
        Application.Quit();
    }

    public void UputeButton()
    {
        if (Upute.activeSelf == false)
        {
            Upute.SetActive(true);
        }
        else
        {
            Upute.SetActive(false);
        }
    }

    public void InfoButton()
    {
        if (Info.activeSelf == false)
        {
            Info.SetActive(true);
        }
        else
        {
            Info.SetActive(false);
        }
    }

    public void PostavkeButton()
    {
        if (Postavke.activeSelf == false)
        {
            Postavke.SetActive(true);
        }
        else
        {
            Postavke.SetActive(false);
        }
    }
}
