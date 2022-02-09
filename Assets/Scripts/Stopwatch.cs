using System;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour //секундомер
{
    public bool isActive;
    public float currentTime;
    [SerializeField] Text timeText;
    public float mathfTime;
    void Start()
    {
        currentTime = 0;
        isActive = true;
    }

    void Update()
    {
        
        if(isActive == true)
        {
            currentTime += Time.deltaTime;
            mathfTime = Mathf.Round(currentTime);
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeText.text = time.Seconds.ToString();
    }
}
