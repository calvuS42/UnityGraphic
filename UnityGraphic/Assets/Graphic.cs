
using UnityEngine;

public class Graphic : MonoBehaviour {

    public Transform pointPrefab;
    [Range (10, 100)]
    public int resolution;
    
	// Use this for initialization
	void Start() {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.z = 0f;
        for (int i = 0; i < resolution; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.SetParent(transform, false);
            position.x = (i + 0.5f)*step - 1f;
            position.y = position.x * position.x;
            point.localPosition = position;
            point.localScale = scale;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
