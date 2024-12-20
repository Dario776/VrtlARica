using UnityEngine;
using UnityEngine.UI;

public class Postavke : MonoBehaviour
{
    [SerializeField] private GameObject OpcijaFonta1;
    [SerializeField] private GameObject OpcijaFonta2;
    private Image imageOpcijaFonta1;
    private Image imageOpcijaFonta2;

    private PostavkeManager postavkeManager;

    private void Start()
    {
        postavkeManager = PostavkeManager.Instance;
        imageOpcijaFonta1 = OpcijaFonta1.GetComponent<Image>();
        imageOpcijaFonta2 = OpcijaFonta2.GetComponent<Image>();
        Refresh();
    }

    private void Refresh()
    {
        if (postavkeManager.currentFont == "Sans")
            ChangeFontSans();
        else
            ChangeFontDyslexic();
    }

    public void ChangeFontSans()
    {
        imageOpcijaFonta1.color = Konstante.CustomOrangeColor;
        imageOpcijaFonta2.color = Konstante.WhiteColor;
    }
    public void ChangeFontDyslexic()
    {
        imageOpcijaFonta1.color = Konstante.WhiteColor;
        imageOpcijaFonta2.color = Konstante.CustomOrangeColor;
    }
    public void ChangeFontSansButton()
    {
        postavkeManager.ChangeFontSans();
        imageOpcijaFonta1.color = Konstante.CustomOrangeColor;
        imageOpcijaFonta2.color = Konstante.WhiteColor;
    }
    public void ChangeFontDyslexicButton()
    {
        postavkeManager.ChangeFontDyslexic();
        imageOpcijaFonta1.color = Konstante.WhiteColor;
        imageOpcijaFonta2.color = Konstante.CustomOrangeColor;
    }
    public void CloseButton()
    {
        gameObject.SetActive(false);
    }
}
