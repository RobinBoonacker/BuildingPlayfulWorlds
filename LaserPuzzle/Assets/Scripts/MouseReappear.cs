using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseReappear : MonoBehaviour {

    public CursorLockMode wantedState;

	// Use this for initialization
	void Start () {
        Cursor.lockState = wantedState;
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
