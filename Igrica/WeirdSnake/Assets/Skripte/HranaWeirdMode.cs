using Assets.Skripte;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HranaWeirdMode : MonoBehaviour, IUpravljanjeHranom
{

    public GameObject plus;
    public GameObject minus;

    // Use this for initialization
    void Start()
    {
        dajNovu();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void dajMinus()
    {
        int x, y;
        while (true)
        {
            x = Random.Range(1, 31);
            y = Random.Range(-15, 15);
            break;
        }
        minus.transform.position = new Vector3(x, y, 0);

        Instantiate(minus, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void dajNovu()
    {
        int x, y;
        while (true)
        {
            x = Random.Range(1, 31);
            y = Random.Range(-15, 15);
            break;
        }
        plus.transform.position = new Vector3(x, y, 0);

        Instantiate(plus, new Vector3(x, y, 0), Quaternion.identity);

    }

    public void pojedi()
    {
        GameObject[] hrana = GameObject.FindGameObjectsWithTag("Hrana");
        foreach (GameObject h in hrana)
        {
            Destroy(h);
        }

    }
}
