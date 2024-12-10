using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{

    [SerializeField]
    private GameObject Hint;
    [SerializeField]
    private GameObject Postavke;
    [SerializeField]
    private GameObject IzlazPopup;
    [SerializeField]
    private GameObject DropdownMenu;
    [SerializeField]
    private GameObject Zamrzni;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
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
        if (Time.timeScale > 0)
        {
            Zamrzni.GetComponent<Image>().color = new Color32(141, 225, 249, 255);
            Time.timeScale = 0;
        }
        else
        {
            Zamrzni.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            Time.timeScale = 1;
        }
    }
}
