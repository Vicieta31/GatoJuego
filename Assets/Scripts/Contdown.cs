using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    // Start is called before the first frame update

    float maxTime;
    float currentTime;
    bool isDone = false;
    bool hasStarted = false;
    public void SetCuntdown(float startTime)
    {
        maxTime = startTime;
        currentTime = maxTime;
        isDone = false;
        hasStarted = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0.0f)
            {
                isDone = true;
                hasStarted = false;
            }
        }
    }
    public void StartTimer()
    {
        hasStarted = true;
    }
    public void StopTimer()
    {
        hasStarted = false;
    }
    public void ResetTimer()
    {
        currentTime = maxTime;
        isDone = false;
    }
    public bool HasCompleted()
    {
        return isDone;
    }
}
