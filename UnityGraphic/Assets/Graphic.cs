
using UnityEngine;

public class Graphic : MonoBehaviour {

    public Transform pointPrefab;
        

	// Use this for initialization
	void Start() {
        for (int i = 0; i < 10; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.localPosition = Vector3.right * i;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
