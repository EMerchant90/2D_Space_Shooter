using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float _speed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //move up at 10 speed
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //if position on the y is greater than or equal to 6 
        //destroy laser
        if (transform.position.y >= 6)
        {
            Destroy(gameObject);
        }
    }
}
