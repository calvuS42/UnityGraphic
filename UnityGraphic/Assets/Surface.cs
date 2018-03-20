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

    static Vector3 SineFunc(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.z = z;
        p.y = Mathf.Sin(Mathf.PI * (x + t));
        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.z = z;
        p.y = Mathf.Sin(Mathf.PI * (x + t));
        p.y += Mathf.Sin(Mathf.PI * (z + t));
        p.y *= 0.5f;
        return p;
    }


    static Vector3 MultiSineFunc(float x,float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.z = z;
        p.y = Mathf.Sin(Mathf.PI * (x + t));
        p.y += Mathf.Sin(2f * Mathf.PI * (x + 2f*t)) / 2f;
        p.y *= 2f / 3f;
        return p;
    }

    static Vector3 MultiSine2DFunction (float x, float z, float t) {
        Vector3 p;
        p.x = x;
        p.z = z;
        p.y = 4f * Mathf.Sin(Mathf.PI * (x + z + t * 0.5f));
		p.y += Mathf.Sin(Mathf.PI * (x + t));
		p.y += Mathf.Sin(2f * Mathf.PI * (z + 2f * t)) * 0.5f;
		p.y *= 1f / 5.5f;
		return p;
	}

    static Vector3 Ripple(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.z = z;
        float d = Mathf.Sqrt(x * x + z * z);
        p.y = Mathf.Sin(Mathf.PI*(4f * d - t));
        p.y /= 1f + 10f * d;
        return p;
    }

    static Vector3 Cylinder(float u, float v, float t)
    {
        float r = 0.8f + Mathf.Sin(Mathf.PI * (6f * u + 2f * v + t)) * 0.2f;
        Vector3 p;
        p.x = r * Mathf.Sin(Mathf.PI * u);
        p.y = v;
        p.z = r * Mathf.Cos(Mathf.PI * u); ;
        return p;
    }

    static Vector3 Sphere(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(Mathf.PI * (6f * u + t)) * 0.1f;
        r += Mathf.Sin(Mathf.PI * (4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(Mathf.PI * 0.5f * v);
        p.x = s * Mathf.Sin(Mathf.PI * u);
        p.y = r * Mathf.Sin(Mathf.PI * 0.5f * v);
        p.z = s * Mathf.Cos(Mathf.PI * u);
        return p;
    }

    static Vector3 Torus(float u, float v, float t)
    {
        Vector3 p;
        float r1 = 0.65f + Mathf.Sin(Mathf.PI * (6f * u + t)) * 0.1f;
        float r2 = 0.2f + Mathf.Sin(Mathf.PI * (4f * v + t)) * 0.05f;
        float s = r2 * Mathf.Cos(Mathf.PI * v) + r1;
        p.x = s * Mathf.Sin(Mathf.PI * u);
        p.y = r2 * Mathf.Sin(Mathf.PI * v);
        p.z = s * Mathf.Cos(Mathf.PI * u);
        return p;
    }


    SurfaceFunction[] functions = {
                SineFunc, Sine2DFunction, MultiSineFunc, MultiSine2DFunction, Ripple,
                Cylinder, Sphere, Torus

    };



    // Use this for initialization
    void Start()
    {
        points = new Transform[resolution*resolution];
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.y = 0f;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;
        float step = 2f / resolution;
        SurfaceFunction f = functions[(int)function];
        for (int i = 0, z = 0; z < resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++)
            {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }
        }
    }
}
