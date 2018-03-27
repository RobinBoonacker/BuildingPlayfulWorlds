using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour {

    public string finishedMenu;
    private bool isActivated;
    public Material onMaterial;
    public Material offMaterial;

    // Use this for initialization
    void Start () {
        isActivated = false;
        GetComponent<Renderer>().material = offMaterial;
        GetComponent<Light>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (this.tag == "Teleporter-on" && !isActivated)
        {
            print("Telporter has been activated!");
            isActivated = true;

            GetComponent<Light>().enabled = true;
            GetComponent<Renderer>().material = onMaterial;
        } else
        {
            if (this.tag == "Teleporter" && isActivated)
            {
                print("Telporter has been deactivated!");
                isActivated = false;

                GetComponent<Light>().enabled = false;
                GetComponent<Renderer>().material = offMaterial;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (isActivated)
        {
            print("You finished the game!");
            SceneManager.LoadScene(finishedMenu);
        }
    }
}
