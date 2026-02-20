using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager Instance { get; private set; }

    SwipeData data = new SwipeData();
    [SerializeField] int maxPointsCapacity = 10;
    [SerializeField] float refreshTimer = 0.1f, timer;

    public Action<SwipeData> OnSwipe;
    public Action<SwipeData> OnSwipeEnd; // nuevo

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.touchCount < 1) return;

        var touch = Input.touches[0];

        if (touch.phase == TouchPhase.Began)
        {
            data.isSwipe = true;
            data.points.Clear();
            data.points.Add(touch.position);
            timer = 0;
            return;
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            if (data.points.Count > 0)
            {
                OnSwipeEnd?.Invoke(data);
            }
            data.isSwipe = false;
            data.points.Clear();
            timer = 0;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= refreshTimer)
        {
            data.points.Add(touch.position);

            if (data.points.Count > maxPointsCapacity)
            {
                data.points.RemoveAt(0);
            }
            timer = 0;
            OnSwipe?.Invoke(data);
        }
    }
}