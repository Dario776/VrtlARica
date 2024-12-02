using UnityEngine;

public class Postavke : MonoBehaviour
{
    public GameObject Naslovnica;
    public void ShowNaslovnica()
    {
        Naslovnica.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
