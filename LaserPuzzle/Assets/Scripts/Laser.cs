using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public GameObject turret;
    public Material laserMaterial;
    public AudioClip laserSound;

    private Vector3[] points;
    private List<GameObject> laserObjects;

    // Use this for initialization
    void Start () {
        points = new Vector3[0];
        laserObjects = new List<GameObject>();
        turret.AddComponent<AudioSource>();
        turret.GetComponent<AudioSource>().clip = laserSound;
        turret.GetComponent<AudioSource>().loop = true;
    }
	
	// Update is called once per frame
	void Update () {
        TurnOffTeleporters();
        ClearPoints();
        if (turret.tag == "Turret")
        {
            Vector3 direction = transform.forward;
            AddPoint(transform.position);
            while (direction != Vector3.zero)
            {
                direction = ContinueRay(direction);
            }

            if (!turret.GetComponent<AudioSource>().isPlaying)
            {
                turret.GetComponent<AudioSource>().Play();
            }
            DrawLaser();
        } else
        {
            turret.GetComponent<AudioSource>().Stop();
            clearLaser();
        }
    }

    void TurnOffTeleporters()
    {
        GameObject[] teleporters = GameObject.FindGameObjectsWithTag("Teleporter-on");

        foreach (GameObject teleporter in teleporters)
        {
            teleporter.tag = "Teleporter";
        }
    }

    void OnReceiverHit()
    {
        GameObject[] teleporters = GameObject.FindGameObjectsWithTag("Teleporter");

        foreach (GameObject teleporter in teleporters)
        {
            teleporter.tag = "Teleporter-on";
        }
    }

    Vector3 ContinueRay (Vector3 direction)
    {
        Vector3 newDirection = Vector3.zero;
        Vector3 start = points[points.Length - 1];

        Ray ray = new Ray(start, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            AddPoint(hit.point);
            if (hit.transform.tag == "Mirror")
            {
                newDirection = Vector3.Reflect(direction, hit.normal);
            } else if (hit.transform.tag == "receiver")
            {
                OnReceiverHit();
            }
        } else
        {
            direction.Scale(new Vector3(100, 100, 100));
            AddPoint(start + direction);
        }

        return newDirection;
    }

    void ClearPoints ()
    {
        points = new Vector3[0];
    }

    void AddPoint (Vector3 point)
    {
        Vector3[] newPoints = new Vector3[points.Length + 1];
        for (int i = 0; i < points.Length; i++)
        {
            newPoints[i] = points[i];
        }
        newPoints[points.Length] = point;
        points = newPoints;
    }

    void clearLaser()
    {
        for (int i = 0; i < laserObjects.Count; i++)
        {
            Destroy(laserObjects[i]);
        }
    }

    void DrawLaser ()
    {
        clearLaser();
        for (int i = 0; i < points.Length - 1; i++)
        {
            Debug.DrawLine(points[i], points[i + 1], Color.blue);

            // spawn objects
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.GetComponent<BoxCollider>().enabled = false;
            cube.GetComponent<MeshRenderer>().enabled = false;
            cube.AddComponent<LineRenderer>();
            LineRenderer lineRenderer = cube.GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.2f;
            lineRenderer.endWidth = 0.2f;
            lineRenderer.SetPosition(0, points[i]);
            lineRenderer.SetPosition(1, points[i + 1]);
            lineRenderer.material = laserMaterial;

            laserObjects.Add(cube);
        }
    }
}