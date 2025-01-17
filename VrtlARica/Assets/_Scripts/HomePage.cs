using UnityEngine;

public class HomePage : MonoBehaviour
{
    [SerializeField] private GameObject Instructions;
    [SerializeField] private GameObject Info;
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject ExitPopup;
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

    public void StartButton()
    {
        audioManager.Play("startbutton");
        gameManager.LoadGameScene();
    }

    public void ExitButton()
    {
        audioManager.Play("startbutton");
        ExitPopup.SetActive(true);
    }

    public void CancelExit()
    {
        audioManager.Play("startbutton");
        ExitPopup.SetActive(false);
    }

    public void ConfirmExit()
    {
        audioManager.Play("startbutton");
        Application.Quit();
    }

    public void InstructionsButton()
    {
        audioManager.Play("startbutton");
        if (Instructions.activeSelf == false)
        {
            Instructions.SetActive(true);
        }
        else
        {
            Instructions.SetActive(false);
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

    public void SettingsButton()
    {
        audioManager.Play("startbutton");
        if (Settings.activeSelf == false)
        {
            Settings.SetActive(true);
        }
        else
        {
            Settings.SetActive(false);
        }
    }
}
