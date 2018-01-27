using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum InputGesture
{
    Unknown,
    Tap,
    Swipe,
    Hold
}

[System.Serializable]
public struct InputGestureData
{
    public InputGesture Gesture;
    private bool _closed;
}
#pragma warning disable 0414

public class InputGestureReader : Graphic, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public InputGestureData LastGesture;

    private float _holdTime = 1.5f;
    private float _tapTime = 0.5f;
    private float _swipeTime = 1.5f;

    private bool _pointerDown;
    private float _pointerDownTime;
    private Vector2 _pointerDownPos;
    private Vector2 _beginDragPos;
    private Vector2 _endDragPos;

    private bool _timerActive;

    private float _timerStart;
    private float _timerStop;

    private float _timerValue;

    private bool _tapCandidate = true;
    private bool _holdCandidate = true;
    private bool _swipeCandidate = false;

    private InputGestureData _activeGestureData;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartTimer();
        _pointerDown = true;
        _activeGestureData = new InputGestureData();
        _activeGestureData.Gesture = InputGesture.Unknown;
        _pointerDownPos = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _beginDragPos = eventData.position;
        _swipeCandidate = true;
        _tapCandidate = false;
        _holdCandidate = false;
        StartTimer();
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var stopTimer = StopTimer();

        if (stopTimer >= _tapTime)
            _tapCandidate = false;

        if (stopTimer <= _holdTime)
            _holdCandidate = false;

        if (stopTimer >= _swipeTime)
            _swipeCandidate = false;


        if (_tapCandidate)
        {
            _activeGestureData.Gesture = InputGesture.Tap;
        }
        if (_holdCandidate)
        {
            _activeGestureData.Gesture = InputGesture.Hold;
        }
        if (_swipeCandidate)
        {
            _endDragPos = eventData.position;
            _activeGestureData.Gesture = InputGesture.Swipe;
        }

        LastGesture = _activeGestureData;
        ResetGesture();
    }

    private void StartTimer()
    {
        ResetTimer();
        _timerActive = true;
        StartCoroutine(TickTimer());

    }

    private float StopTimer()
    {
        StopCoroutine("TickTimer");
        _timerActive = false;
        return _timerValue;
    }

    private void ResetTimer()
    {
        StopCoroutine("TickTimer");
        _timerActive = false;
        _timerValue = 0;
    }

    private IEnumerator TickTimer()
    {
        while (_timerActive)
        {
            yield return new WaitForSeconds(0.1f);
            _timerValue += 0.1f;
        }
    }

    private void ResetGesture()
    {
        ResetTimer();
        _activeGestureData = new InputGestureData();
        _tapCandidate = true;
        _holdCandidate = true;
        _swipeCandidate = false;
    }
}
