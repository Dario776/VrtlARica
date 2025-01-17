using UnityEngine;

public class Instructions : MonoBehaviour
{
    [SerializeField] private GameObject Instructions1;
    [SerializeField] private GameObject Instructions2;
    [SerializeField] private GameObject Instructions3;
    [SerializeField] private GameObject Dot1;
    [SerializeField] private GameObject Dot2;
    [SerializeField] private GameObject Dot3;

    private int currentInstructions;

    private void Awake()
    {
        currentInstructions = 1;
    }

    private void OnEnable()
    {
        currentInstructions = 1;
        ShowInstruction(currentInstructions);
    }

    public void ShowNextInstruction()
    {
        if (currentInstructions == 3)
            return;

        currentInstructions++;
        ShowInstruction(currentInstructions);
    }

    public void ShowLastInstruction()
    {
        if (currentInstructions == 1)
            return;

        currentInstructions--;
        ShowInstruction(currentInstructions);
    }

    private void ShowInstruction(int currentInstruction)
    {
        AudioManager.Instance.Play("startbutton");
        if (currentInstruction == 1)
        {
            Instructions1.SetActive(true);
            Instructions2.SetActive(false);
            Instructions3.SetActive(false);
            Dot1.SetActive(false);
            Dot2.SetActive(true);
            Dot3.SetActive(true);
        }
        else if (currentInstruction == 2)
        {
            Instructions1.SetActive(false);
            Instructions2.SetActive(true);
            Instructions3.SetActive(false);
            Dot1.SetActive(true);
            Dot2.SetActive(false);
            Dot3.SetActive(true);
        }
        else
        {
            Instructions1.SetActive(false);
            Instructions2.SetActive(false);
            Instructions3.SetActive(true);
            Dot1.SetActive(true);
            Dot2.SetActive(true);
            Dot3.SetActive(false);
        }
    }
}
