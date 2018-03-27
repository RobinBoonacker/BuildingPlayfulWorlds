using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour {

    public GameObject rotateObject;
    public GameObject pivot;

    private bool keyQ;
    private bool keyE;

	// Use this for initialization
	void Start () {
        keyQ = false;
        keyE = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q"))
        {
            keyQ = true;
        } else if(Input.GetKeyUp("q")) {
            keyQ = false;
        }

        if (Input.GetKeyDown("e"))
        {
            keyE = true;
        }
        else if (Input.GetKeyUp("e")) {
            keyE = false;
        }

        if (keyQ)
        {
            transform.RotateAround(pivot.transform.position, Vector3.up, -10 * Time.deltaTime);
        } else if (keyE)
        {
            transform.RotateAround(pivot.transform.position, Vector3.up, 10 * Time.deltaTime);
        }
    }
}
