using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlipButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public Flip mFlipperScript1;
    public Flip mFlipperScript2;

    bool _holding;
    float _lastHoldTime;

    public void OnPointerDown(PointerEventData eventData)
    {
        _holding = true;
        _lastHoldTime = Time.time;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _holding = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _holding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_holding)
        {
            mFlipperScript1.HoldingLeft();
            mFlipperScript2.HoldingLeft();
            mFlipperScript1.HoldingRight();
            mFlipperScript2.HoldingRight();
        }
    }
}
