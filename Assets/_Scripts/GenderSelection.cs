using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GenderSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameManager GM;
    public Animator characterAnimator;

    public void SelectMale()
    {
        GM.saveGender(0);
        GM.LoadGender();
        SceneManager.LoadScene("Map");
    }

    public void SelectFemale()
    {
        GM.saveGender(1);
        GM.LoadGender();
        SceneManager.LoadScene("Map");
    }

    public void SelectOther()
    {
        GM.saveGender(2);
        GM.LoadGender();
        SceneManager.LoadScene("Map");
    }

    public void OnPointerEnter(PointerEventData eventData)
     {
         if (characterAnimator != null)
         {
             Debug.Log("Hey");
             characterAnimator.SetTrigger("Walk");
         }
     }

     public void OnPointerExit(PointerEventData eventData)
     {
         if (characterAnimator != null)
         {
             characterAnimator.SetTrigger("Idle");
         }
     }
}