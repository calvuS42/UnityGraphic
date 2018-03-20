using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    public Transform pointPrefab;

    public SurfaceFunctionName function;
    [Range(10, 100)]
    public int resolution;

    Transform[] points;

    static float SineFunc(float x, float z, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }

    static float Sine2DFunction(float x, float z, float t)
    {
        float y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(Mathf.PI * (z + t));
        y *= 0.5f;
        return y;
    }


    static float MultiSineFunc(float x,float z, float t)
    {
        float y = SineFunc(x, z, t);
        y += Mathf.Sin(2f * Mathf.PI * (x + 2f*t)) / 2f;
        y *= 2f / 3f;
        return y;
    }

    static float MultiSine2DFunction (float x, float z, float t) {
		float y = 4f * Mathf.Sin(Mathf.PI * (x + z + t * 0.5f));
		y += Mathf.Sin(Mathf.PI * (x + t));
		y += Mathf.Sin(2f * Mathf.PI * (z + 2f * t)) * 0.5f;
		y *= 1f / 5.5f;
		return y;
	}

    static float Ripple(float x, float z, float t)
    {
        float d = Mathf.Sqrt(x * x + z * z);
        float y = d;
        return y;
    }


    SurfaceFunction[] functions = {
                SineFunc, Sine2DFunction, MultiSineFunc, MultiSine2DFunction

    };



    // Use this for initialization
    void Start()
    {
        points = new Transform[resolution*resolution];
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.y = 0f;
        for (int i = 0, z = 0; z < resolution; z++)
        {
            position.z = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++)
            {
                Transform point = Instantiate(pointPrefab);
                position.x = (x + 0.5f) * step - 1f;
                
                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false);
                points[i] = point;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            float t = Time.time;
            Transform point = points[i];
            Vector3 position = point.localPosition;
            SurfaceFunction f = functions[(int)function];
            position.y = f(position.x, position.z, t);
            point.localPosition = position;
        }
    }
}
