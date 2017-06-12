using Assets.Skripte;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZmijaNormalMode : MonoBehaviour, IHrana, IPokret, ISudar
{
    public List<GameObject> dijeloviZmije;
    public GameObject glavaZmije;
    public GameObject repZmije;
    private string smjer;
    private string prethodniSmjer;
    private float coolDown = 0;
    bool uzelaJeHranu = false;
    Vector3 temp;
  

    // Use this for initialization
    void Start()
    {
        smjer = "LIJEVO";
        prethodniSmjer = "LIJEVO";
       // HranaNormalMode hnm = glavaZmije.AddComponent<HranaNormalMode>();
       
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && prethodniSmjer != "DESNO") // ne smiju se zamijeniti rep i g l a v a
        {
            smjer = "LIJEVO";
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) && prethodniSmjer != "LIJEVO")
        {
            smjer = "DESNO";
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) && prethodniSmjer != "DOLE")
        { 
            smjer = "GORE";
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) && prethodniSmjer != "GORE")
        {
            smjer = "DOLE";
        }

        else if (Time.time - coolDown >= 0.075f)
        {
            pomjeriZmiju();
            coolDown = Time.time;
            prethodniSmjer = smjer;
        }
        //coolDown -= Time.deltaTime;
    }

    public void pomjeriZmiju()
    {
        if (!zmijaJeUdarilaUZid() && !zmijaSeDodiruje())
        {

            if (zmijaJeNaislaNaHranu())
            {
                uzmiHranu();
                pomjeriTijeloZmije();


            }
            else
            {
                uzelaJeHranu = false;
                pomjeriTijeloZmije();
            }
            // pomjeri joj i glavu na kraju (prvi segment tijela dodje na mjesto glave)
            switch (smjer)
            {
                case "LIJEVO":
                    glavaZmije.transform.position += new Vector3(-1f, 0, 0);
                    break;
                case "DESNO":
                    glavaZmije.transform.position += new Vector3(1f, 0, 0);
                    break;
                case "GORE":
                    glavaZmije.transform.position += new Vector3(0, 1f, 0);
                    break;
                case "DOLE":
                    glavaZmije.transform.position += new Vector3(0, -1f, 0);
                    break;
            }
        }
        else
        {
            //FindObjectOfType<Rezultat>().Text.text = "KRAJ IGRE!";
            FindObjectOfType<Rezultat>().displayMenu();
        }
    }

    public void pomjeriTijeloZmije()
    {
        Vector3 glava = glavaZmije.transform.position;
        for (int i = dijeloviZmije.Count - 1; i > 0; i--)
        {
            if (uzelaJeHranu && i == dijeloviZmije.Count - 1)
            { // ako je tek uzela hranu, ne pomjerati posljednji dio!!!
                uzelaJeHranu = false;
                continue;
            }
            dijeloviZmije[i].transform.position = dijeloviZmije[i - 1].transform.position;
        }
        dijeloviZmije[0].transform.position = glava;
    }

    public void uzmiHranu()
    {
        uzelaJeHranu = true;
        dijeloviZmije.Add((GameObject)Instantiate(dijeloviZmije[dijeloviZmije.Count - 1], glavaZmije.transform.position, Quaternion.identity));

        //if (HranaNormalMode.isLoaded)
        {

            //FindObjectOfType<HranaNormalMode>().pojedi();
            HranaNormalMode hnm = glavaZmije.AddComponent<HranaNormalMode>();
            hnm.pojedi();
            if (GameObject.FindGameObjectWithTag("HranaObicna") == null)
            hnm.dajNovu();
            FindObjectOfType<Rezultat>().Text.text = "Obaveze: " + ++FindObjectOfType<Rezultat>().rezultat;
        }
    }

    public bool zmijaJeNaislaNaHranu()
    {
        GameObject hrana = null;
        /*HranaNormalMode hnm = glavaZmije.GetComponent<HranaNormalMode>();
        if (hnm == null) hrana = FindObjectOfType<HranaNormalMode>().hrana;
        else hrana = hnm.hrana;*/
        hrana = FindObjectOfType<HranaNormalMode>().hrana;
        //if (FindObjectOfType<HranaNormalMode>() != null)
           // hrana = FindObjectOfType<HranaNormalMode>().hrana;
        if (hrana && hrana.transform.position.x == glavaZmije.transform.position.x && hrana.transform.position.y == glavaZmije.transform.position.y)
        {
            return true;
        }
        return false;
    }

    public bool zmijaJeUdarilaUZid()
    {
        Vector3 glava = glavaZmije.transform.position;
        if (!(glava.x >= 1 && glava.x <= 31 && glava.y >= -15 && glava.y <= 15))
            return true;
        return false;
    }

    public bool zmijaSeDodiruje()
    {
        Vector3 glava = glavaZmije.transform.position;
        for (int i = 0; i < dijeloviZmije.Count; i++)
        {
            if (dijeloviZmije[i].transform.position.x == glava.x && dijeloviZmije[i].transform.position.y == glava.y)
                return true;
        }
        return false;
    }
}
