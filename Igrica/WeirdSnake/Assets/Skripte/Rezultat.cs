using UnityEngine;
using UnityEngine.UI;

public class Rezultat : MonoBehaviour
{
    public Text Text;
    public int rezultat;

    // Use this for initialization
    void Start()
    {
        rezultat = 3;
        Text.text = "Obaveze: " + rezultat.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
