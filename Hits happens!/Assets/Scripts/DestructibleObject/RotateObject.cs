using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public Joystick joystic;

    [SerializeField] public float rotationMultiplier = 1f;

	
	// Update is called once per frame
	void Update () {

	    if (joystic.Horizontal != 0f || joystic.Vertical != 0)
	    {
	        gameObject.transform.rotation *= Quaternion.Euler(joystic.Horizontal*rotationMultiplier, joystic.Vertical* rotationMultiplier, 0f);
	    }
		
	}
}
