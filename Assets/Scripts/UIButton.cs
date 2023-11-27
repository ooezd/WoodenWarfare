using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public Action onClick;
    public Action onHold;
    public Action onRelease;
    public Action onTouch;

    bool isSelected;
    public Image buttonImage;
    public float pressedScale;
    public Color activeColor;
    public Color pressedColor;
    public Color disabledColor = Color.grey;

    //[SerializeField] FXClip clickSound = FXClip.None;
    //[SerializeField] FXClip holdSound = FXClip.None;
    //[SerializeField] FXClip releaseSound = FXClip.None;
    //[SerializeField] FXClip touchSound = FXClip.None;

    //AudioManager audioManager;

    void Awake()
    {
        //audioManager = Locator.Instance.Resolve<AudioManager>();
    }
    public bool Interactable
    {
        get { return interactable; }
        set
        {
            interactable = value;
            DisplayInteractable();
        }
    }
    bool interactable = true;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable)
        {
            return;
        }

        if (!isSelected)
        {
            onTouch?.Invoke();
        }

        onHold?.Invoke();
        //audioManager.PlayFx(holdSound);
        isSelected = true;
        buttonImage.color = pressedColor;
        buttonImage.transform.localScale = Vector3.one * pressedScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interactable)
        {
            return;
        }

        onRelease?.Invoke();
        //audioManager.PlayFx(releaseSound);
        isSelected = false;
        buttonImage.color = activeColor;
        buttonImage.transform.localScale = Vector3.one;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable)
        {
            return;
        }

        if (isSelected)
        {
            onClick?.Invoke();
            onRelease?.Invoke();

            //audioManager.PlayFx(clickSound);
            //audioManager.PlayFx(releaseSound);

            buttonImage.color = activeColor;
            buttonImage.transform.localScale = Vector3.one;
        }
    }

    void DisplayInteractable()
    {
        buttonImage.color = interactable ? activeColor : disabledColor;
        buttonImage.transform.localScale = Vector3.one;
    }
}
