using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponDrop : MonoBehaviour,IInteractable {

    public string weaponName;
    public float dropChance;

    Vector3 originalPos;
    private void Start()
    {

        //TODO: fix the bobbing such that it is not on the y plane since when the weapon drops on the side it bobs the wrong way
        originalPos = transform.position;
        float displacement = 0.1f;
        float duration = 0.15f;
        Sequence bobbingSequence = DOTween.Sequence();

        bobbingSequence.Append(transform.DOMoveY(originalPos.y + displacement, duration))
            .Append(transform.DOMoveY(originalPos.y, duration))
            .Append(transform.DOMoveY(originalPos.y - displacement, duration))
            .Append(transform.DOMoveY(originalPos.y, duration));
        bobbingSequence.SetLoops(int.MaxValue);

        //bobbingSequence.SetEase(Ease.InSine);

    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Player.Instance.EquipWeapon(weaponName);
            gameObject.SetActive(false);
        }
    }
    

}
