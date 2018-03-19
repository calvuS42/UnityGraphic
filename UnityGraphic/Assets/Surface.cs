using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    public Transform pointPrefab;

    [Range(0, 1)]
    public int function;
    [Range(10, 100)]
    public int resolution;

    Transform[] points;

    float SineFunc(float x, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }


    float MultiSineFunc(float x, float t)
    {
        float y = SineFunc(x, t);
        y += Mathf.Sin(2f * Mathf.PI * (x + 2f*t)) / 2f;
        y *= 2f / 3f;
        return y;
    }


    // Use this for initialization
    void Start()
    {
        points = new Transform[resolution];
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.y = 0f;
        position.z = 0f;
        for (int i = 0; i < resolution; i++)
        {

            Transform point = Instantiate(pointPrefab);

            point.SetParent(transform, false);
            position.x = (i + 0.5f) * step - 1f;


            // position.y = position.x * position.x; // в апдейте создается "динамическая" функция


            point.localPosition = position;
            point.localScale = scale;
            points[i] = point;
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < resolution; i++)
        {
            float t = Time.time;
            Transform point = points[i];
            Vector3 position = point.localPosition;
            if (function == 0) position.y = SineFunc(position.x, t);
            else position.y = MultiSineFunc(position.x, t);
            point.localPosition = position;
        }
    }
}
