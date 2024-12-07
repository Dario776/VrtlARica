using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

public class Naslovnica : MonoBehaviour
{
    public GameObject Upute;
    public GameObject Info;
    public GameObject Postavke;
    public GameObject PopupZaIzlaze;

    public void Start()
    {
        PostavkeManager.Instance.CheckForText();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        PopupZaIzlaze.SetActive(true);
    }

    public void CancelExit()
    {
        PopupZaIzlaze.SetActive(false);
    }

    public void ConfirmExit()
    {
        Application.Quit();
    }

    public void ShowUpute()
    {
        Upute.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ShowInfo()
    {
        Info.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ShowPostavke()
    {
        Postavke.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void ShowPostavkePopup()
    {
        Postavke.SetActive(true);
    }

}
