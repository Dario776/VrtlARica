using UnityEngine;

public class Info : MonoBehaviour
{
    public GameObject Naslovnica;
    public void ShowNaslovnica()
    {
        Naslovnica.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
