using UnityEngine;

public class Upute : MonoBehaviour
{
    [SerializeField] private GameObject Upute1;
    [SerializeField] private GameObject Upute2;
    [SerializeField] private GameObject Upute3;
    [SerializeField] private GameObject Tockica1;
    [SerializeField] private GameObject Tockica2;
    [SerializeField] private GameObject Tockica3;

    private int trenutneUpute;

    private void Awake()
    {
        trenutneUpute = 1;
    }

    private void OnEnable()
    {
        trenutneUpute = 1;
        ShowUpute(trenutneUpute);
    }

    public void ShowNextUpute()
    {
        if (trenutneUpute == 3)
            return;

        trenutneUpute++;
        ShowUpute(trenutneUpute);
    }

    public void ShowLastUpute()
    {
        if (trenutneUpute == 1)
            return;

        trenutneUpute--;
        ShowUpute(trenutneUpute);
    }

    private void ShowUpute(int trenutneUpute)
    {
        if (trenutneUpute == 1)
        {
            Upute1.SetActive(true);
            Upute2.SetActive(false);
            Upute3.SetActive(false);
            Tockica1.SetActive(false);
            Tockica2.SetActive(true);
            Tockica3.SetActive(true);
        }
        else if (trenutneUpute == 2)
        {
            Upute1.SetActive(false);
            Upute2.SetActive(true);
            Upute3.SetActive(false);
            Tockica1.SetActive(true);
            Tockica2.SetActive(false);
            Tockica3.SetActive(true);
        }
        else
        {
            Upute1.SetActive(false);
            Upute2.SetActive(false);
            Upute3.SetActive(true);
            Tockica1.SetActive(true);
            Tockica2.SetActive(true);
            Tockica3.SetActive(false);
        }
    }
}
