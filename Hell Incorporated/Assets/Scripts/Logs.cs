using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logs : MonoBehaviour
{
    public OVRManager OVRManager;

    float deltaTime = 0.0f;

    private void Awake()
    {
        StartCoroutine(SetFrequency());
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        Debug.Log("FPS: " + fps);
    }

    IEnumerator SetFrequency()
    {
        yield return new WaitUntil(() => OVRManager.display.displayFrequenciesAvailable.Length > 0);

        float displayFreq = OVRManager.display.displayFrequency;
        foreach (float freq in OVRManager.display.displayFrequenciesAvailable)
        {
            if (freq > displayFreq)
            {
                displayFreq = freq;
            }
        }
        OVRManager.display.displayFrequency = displayFreq;
    }
}
