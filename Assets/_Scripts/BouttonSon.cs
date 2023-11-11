using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BouttonSon : MonoBehaviour
{
    public AudioClip sonSurvol; // Le son que vous souhaitez jouer
    public AudioClip sonClic;
    private Button bouton;

    void Start()
    {
        bouton = GetComponent<Button>();
        bouton.onClick.AddListener(JouerSonClic);
    }

    void JouerSonClic()
    {
        if (sonClic != null)
        {
            AudioSource.PlayClipAtPoint(sonClic, Camera.main.transform.position);
        }
    }


public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("kiwiiiite");
        JouerSonSurvol();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopperSonSurvol();
    }

    void JouerSonSurvol()
    {
        if (sonSurvol != null)
        {
            Debug.Log("tiwite");
            AudioSource.PlayClipAtPoint(sonSurvol, Camera.main.transform.position);
        }
    }

    void StopperSonSurvol()
    {
        // Arrêtez le son ici si nécessaire
    }
}
