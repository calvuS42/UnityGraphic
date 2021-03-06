﻿
using UnityEngine;

public class Graphic : MonoBehaviour {


    Transform[] points;
    public Transform pointPrefab;
    [Range (10, 100)]
    public int resolution;
    
	// Use this for initialization
	void Start() {
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
            position.x = (i + 0.5f)*step - 1f;


            // position.y = position.x * position.x; // в апдейте создается "динамическая" функция


            point.localPosition = position;
            point.localScale = scale;
            points[i] = point;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < resolution; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y =  Mathf.Sin(Mathf.PI * (position.x + Time.time)) ;
            point.localPosition = position;
        }
	}
}
