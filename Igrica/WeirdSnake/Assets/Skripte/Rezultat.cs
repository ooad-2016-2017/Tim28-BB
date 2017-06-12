using UnityEngine;
using UnityEngine.UI;

public class Rezultat : MonoBehaviour
{
    public GameObject theEndPanel;
    public Text Text;
    public int rezultat;

    // Use this for initialization
    void Start()
    {
        hideMenu();
        rezultat = 3;
        //Text.text = "Obaveze: " + rezultat.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hideMenu()
    {
        theEndPanel.GetComponent<CanvasGroup>().interactable = false;
        theEndPanel.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void displayMenu()
    {
        theEndPanel.GetComponent<CanvasGroup>().interactable = true;
        theEndPanel.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void repeatNormalMode()
    {
        Application.LoadLevel("NormalMode");
    }

    public void exitNormalMode()
    {
        Application.LoadLevel("MenuScena");
    }

    public void repetWeirdMode()
    {
        Application.LoadLevel("WeirdMode");
    }

    public void exitWeirdMode()
    {
        Application.LoadLevel("MenuScena");
    }
}
