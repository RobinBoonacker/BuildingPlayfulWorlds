using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour {

    public float offset;
    public float amplitude;
    public float period;
    public GameObject fireBall;

    private float startTime;

	void Start () {
        startTime = Time.time;
        fireBall.transform.position = new Vector3(0f, offset, 0f);
    }
	
	void Update () {
        float elapsed = Time.time - startTime;
        float newOffset = offset + amplitude * Mathf.Sin(elapsed / period);
        fireBall.transform.position = new Vector3(0f, newOffset, 0f) + gameObject.transform.position;
    }
}
