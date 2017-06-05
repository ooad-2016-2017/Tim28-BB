using Assets.Skripte;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZmijaWeirdMode : MonoBehaviour, IHrana, IPokret, ISudar
{
    public List<GameObject> dijeloviZmije;
    public GameObject glavaZmije;
    public GameObject repZmije;
    private string smjer;
    private string prethodniSmjer;
    private float coolDown = 0;
    private float coolDownAmount = 0.1f;
    bool uzelaJeHranu = false;
    double epsilon = 0.0001;
    Vector3 temp;

    // Use this for initialization
    void Start()
    {
        smjer = "LIJEVO";
        prethodniSmjer = "LIJEVO";
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
            else if (zmijaJeNaislaNaMinus())
            {
                uzmiMinus();
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
            if (dijeloviZmije.Count == 0 && glavaZmije.transform.position.x == -40)
                FindObjectOfType<Rezultat>().Text.text = "Č E S T I T A M O";
            else
                FindObjectOfType<Rezultat>().Text.text = "KRAJ IGRE!";
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
        if (dijeloviZmije.Count > 0) dijeloviZmije[0].transform.position = glava;
    }

    public void uzmiHranu()
    {
        uzelaJeHranu = true;
        dijeloviZmije.Add((GameObject)Instantiate(dijeloviZmije[dijeloviZmije.Count - 1], glavaZmije.transform.position, Quaternion.identity));

        FindObjectOfType<HranaWeirdMode>().pojedi();
        if (FindObjectOfType<Rezultat>().rezultat >= 30 && FindObjectOfType<Rezultat>().rezultat % 6 == 0) FindObjectOfType<HranaWeirdMode>().dajMinus();
        else if (FindObjectOfType<Rezultat>().rezultat > 60) FindObjectOfType<HranaWeirdMode>().dajMinus();
        else FindObjectOfType<HranaWeirdMode>().dajNovu();

        FindObjectOfType<Rezultat>().Text.text = "Obaveze: " + ++FindObjectOfType<Rezultat>().rezultat;
    }

    public void uzmiMinus()
    {
        uzelaJeHranu = true;

        if (dijeloviZmije.Count == 0)
        {
            FindObjectOfType<HranaWeirdMode>().pojedi();
            glavaZmije.transform.SetPositionAndRotation(new Vector3(-40, 0, 0), new Quaternion()); FindObjectOfType<Rezultat>().Text.text = "ČESTITAMO!";
        }
        else
        {
            GameObject dio = dijeloviZmije[dijeloviZmije.Count - 1];
            dijeloviZmije.Remove(dijeloviZmije[dijeloviZmije.Count - 1]);
            dio.transform.position += new Vector3(-100, 100, 0);
            dio.transform.SetPositionAndRotation(new Vector3(-40, 0, 0), new Quaternion());
            GameObject[] dijelovi = GameObject.FindGameObjectsWithTag("DioZmije");
            if (dijelovi.Length > 0) Destroy(dijelovi[0]);

            FindObjectOfType<HranaWeirdMode>().pojedi();
            if (FindObjectOfType<Rezultat>().rezultat >= 3 && FindObjectOfType<Rezultat>().rezultat % 6 == 0) FindObjectOfType<HranaWeirdMode>().dajMinus();
            else if (FindObjectOfType<Rezultat>().rezultat > 6) FindObjectOfType<HranaWeirdMode>().dajMinus();
            else FindObjectOfType<HranaWeirdMode>().dajNovu();

            FindObjectOfType<Rezultat>().Text.text = "Obaveze: " + ++FindObjectOfType<Rezultat>().rezultat;
        }
    }

    public bool zmijaJeNaislaNaHranu()
    {

        GameObject plus = FindObjectOfType<HranaWeirdMode>().plus;
        if (plus != null && plus.transform.position.x == glavaZmije.transform.position.x && plus.transform.position.y == glavaZmije.transform.position.y)
        {
            return true;
        }
        return false;
    }

    bool zmijaJeNaislaNaMinus()
    {
        GameObject minus = FindObjectOfType<HranaWeirdMode>().minus;

        if (minus != null && minus.transform.position.x == glavaZmije.transform.position.x && minus.transform.position.y == glavaZmije.transform.position.y)
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
