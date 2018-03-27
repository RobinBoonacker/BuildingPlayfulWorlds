using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutScript : MonoBehaviour {

    public float fadeTime = 0f;
    public Text text;
    public float red;
    public float green;
    public float blue;
    
    private float startTime;
    
	void Start () {
        startTime = Time.time;
	}
	
	void Update () {
        float elapsed = Time.time - startTime;
        float alpha = 1f - Mathf.Min(elapsed / fadeTime, 1f);
        text.color = new Color(red, green, blue, alpha);
    }
}
