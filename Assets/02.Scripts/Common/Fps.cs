using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Device FramePerSecond Debugger
public class Fps : MonoBehaviour
{
    public float updateInterval = 0.5F;
    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval

    public Text label_fps;
   


    void OnEnable()
    {
       
        timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            float fps = accum / frames;
            string format = System.String.Format("{0:F2} FPS", fps);
            label_fps.text = format;

            if (fps < 30)
                label_fps.color = Color.yellow;
            else
                if (fps < 10)
                label_fps.color = Color.red;
            else
                label_fps.color = Color.green;
            
            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }

}
