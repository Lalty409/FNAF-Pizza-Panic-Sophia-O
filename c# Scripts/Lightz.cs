using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lightz : MonoBehaviour
{
    private Light2D Light;
    // Start is called before the first frame update
    void Start()
    {
        Light = gameObject.GetComponent<Light2D>();
        StartCoroutine(LightIn(0.5f));
    }
    IEnumerator LightIn(float speed)
    {
        float t = 0;

        float temp = 0;
        while (temp < 1)
        {
            t += Time.deltaTime * speed;
            temp = (t*t);
            Light.intensity = temp;
            yield return null;
        }
        Light.intensity = 1;
        this.enabled = false;
    }
}
