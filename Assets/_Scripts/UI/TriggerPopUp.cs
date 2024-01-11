using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPopUp : MonoBehaviour
{
    public ZonePopup zonePopup;
    // Start is called before the first frame update
    void Start()
    {
      //  zonePopup = GetComponent<ZonePopup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("trigger ok");
            // Le joueur entre dans une nouvelle zone
            // Afficher le popup avec le nom de la zone
            zonePopup.ShowPopup(this.name);
        }
    }
}
