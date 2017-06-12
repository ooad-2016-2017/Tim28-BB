using Assets.Skripte;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HranaNormalMode : MonoBehaviour, IUpravljanjeHranom
{
    private GameObject _hrana;
    public GameObject hrana;
    public static bool isLoaded = false;
    public Sprite[] fruitSprites;
   

    // Use this for initialization
    void Start()
    {
        //SceneManager.sceneLoaded += this.OnLoadCallback;
        dajNovu();
    }

    void Awake()
    {
        // load all frames in fruitsSprites array
        
    }

    public void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        isLoaded = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void dajNovu()
    {
        Sprite sp = Resources.Load<Sprite>("noteAsFood");
        hrana = new GameObject();
        hrana.tag = "HranaObicna";
        hrana.AddComponent<SpriteRenderer>();
        hrana.GetComponent<SpriteRenderer>().sprite = sp;
        
        int x, y;
        while (true)
        {
            x = Random.Range(1, 31);
            y = Random.Range(-15, -2);
            Debug.Log(x.ToString() + " " + y.ToString());
            break;
            
        }
        hrana.transform.position = new Vector3(x, y, 0);
        Instantiate(hrana, new Vector3(x, y, 0), Quaternion.identity);
       /* int x, y;
        while (true)
        {
            x = Random.Range(1, 31);
            y = Random.Range(-15, 15);
            break;
        }
        hrana.transform.position = new Vector3(x, y, 0);

        Instantiate(hrana, new Vector3(x, y, 0), Quaternion.identity);*/
    }

    public void pojedi()
    {
        GameObject[] hrana = GameObject.FindGameObjectsWithTag("HranaObicna");
        foreach (GameObject h in hrana)
        {
            Destroy(h);
        }

    }

    void OnLoadCallBack(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("Scene is loaded.");
        isLoaded = true;
    }
}
