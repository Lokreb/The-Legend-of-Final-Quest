using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereIamI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (CounterOfBoss == 0)
        {
            foreach (GameObject background in ListOfBck)
            {
                background.SetActive(false);
            }
            ListOfBck[0].SetActive(true);
        }
        else if (CounterOfBoss == 1)
        {
            foreach (GameObject background in ListOfBck)
            {
                background.SetActive(false);
            }
            ListOfBck[1].SetActive(true);
        }
        else if (CounterOfBoss == 2)
        {
            foreach (GameObject background in ListOfBck)
            {
                background.SetActive(false);
            }
            ListOfBck[2].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int CounterOfBoss =0;
    public GameObject[] ListOfBck;
}
