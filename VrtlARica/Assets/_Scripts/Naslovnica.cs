using UnityEngine;

public class Naslovnica : MonoBehaviour
{
    [SerializeField] private GameObject Upute;
    [SerializeField] private GameObject Info;
    [SerializeField] private GameObject Postavke;
    [SerializeField] private GameObject IzlazPopup;
    [SerializeField] private GameObject sunFrame;
    [SerializeField] private float sunRotationSpeed = 10f;

    private GameManager gameManager;
    private AudioManager audioManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        audioManager.Stop("endsuccess");
        audioManager.Play("mainmenumusic");
    }

    private void Update()
    {
        sunFrame.transform.Rotate(Vector3.forward, sunRotationSpeed * Time.deltaTime);
    }

    public void ZapocniButton()
    {
        audioManager.Play("startbutton");
        gameManager.LoadGameScene();
    }

    public void IzlazButton()
    {
        audioManager.Play("startbutton");
        IzlazPopup.SetActive(true);
    }

    public void CancelExit()
    {
        audioManager.Play("startbutton");
        IzlazPopup.SetActive(false);
    }

    public void ConfirmExit()
    {
        audioManager.Play("startbutton");
        Application.Quit();
    }

    public void UputeButton()
    {
        audioManager.Play("startbutton");
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
        audioManager.Play("startbutton");
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
        audioManager.Play("startbutton");
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
