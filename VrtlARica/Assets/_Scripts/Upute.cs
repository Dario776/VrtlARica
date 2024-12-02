using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upute : MonoBehaviour
{
    private int trenutneUpute;
    public GameObject Naslovnica;
    public GameObject Upute1;
    public GameObject Upute2;
    public GameObject Upute3;

    void Awake()
    {
        trenutneUpute = 1;
    }

    void OnEnable()
    {
        trenutneUpute = 1;
        ShowUpute(trenutneUpute);
    }

    
    public void ShowNaslovnica()
    {
        Naslovnica.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void ShowNextUpute()
    {
        if(trenutneUpute==3)
        return;

        trenutneUpute++;
        ShowUpute(trenutneUpute);
    }
    public void ShowLastUpute()
    {
        if(trenutneUpute==1)
        return;
        
        trenutneUpute--;
        ShowUpute(trenutneUpute);
    }
    private void ShowUpute(int trenutneUpute)
    {
        if(trenutneUpute==1){
            Upute1.SetActive(true);
            Upute2.SetActive(false);
            Upute3.SetActive(false);
        }
        else if(trenutneUpute==2){ 
            Upute1.SetActive(false);
            Upute2.SetActive(true);
            Upute3.SetActive(false);
        }
        else{
            Upute1.SetActive(false);
            Upute2.SetActive(false);
            Upute3.SetActive(true);
        }
     
    }
}
