using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent OnTimeout;

    //public delegate void TimeoutEvent();
    //public TimeoutEvent OnTimeout;

    [SerializeField]
    protected float waittime;
    public float Waittime
    {
        get { return waittime; }
        set { waittime = value; }
    }

    [SerializeField]
    protected bool startOnAwake = false;

    [SerializeField]
    protected bool oneShot = false;

    protected float timeout;

    private bool isActive = false;

    void Start()
    {
        if (startOnAwake)
        {
            SetTimeOut();
        }
    }

    void Update()
    {
        if (isActive && (Time.time > timeout))
        {
            OnTimeOut();
        }
    }

    protected virtual void SetTimeOut()
    {
        timeout = Time.time + waittime;
        isActive = true;
    }

    private void OnTimeOut()
    {
        if (!oneShot)
        {
            SetTimeOut();
        }
        else
        {
            isActive = false;
        }

        OnTimeout?.Invoke();
    }

    public void Stop()
    {
        isActive = false;
    }

    public void Run()
    {
        SetTimeOut();
    }
}
