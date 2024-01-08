using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GenderSelection : MonoBehaviour, IPointerEnterHandler
{

    public GameManager GM;
    public Animator animatorMale;
    public Animator animatorFemale;
    public Animator animatorOther;
    public GameObject self;
    public GameObject game;

    public void SelectMale()
    {
        GM.saveGender(0);
        GM.LoadGender();
        self.SetActive(false);
        game.SetActive(true);

    }

    public void SelectFemale()
    {
        GM.saveGender(1);
        GM.LoadGender();
        self.SetActive(false);
        game.SetActive(true);
    }

    public void SelectOther()
    {
        GM.saveGender(2);
        GM.LoadGender();
        self.SetActive(false);
        game.SetActive(true);
    }

    public void PlayHoverMaleAnimation()
    {
        animatorMale.Play("Male");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor entered the selectable UI element.");
    }

    public void PlayHoverFemaleAnimation()
    {
        animatorFemale.Play("Female");
    }

    public void PlayHoverOtherAnimation()
    {
        animatorOther.Play("Other");
    }
}