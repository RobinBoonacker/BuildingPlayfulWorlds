using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastClick : MonoBehaviour
{
    public int InteractionButton = 0;
    public int SecondaryInteractionButton = 1;
    public float rotationSpeed = 1f;
    public GameObject turretSource;

    private List<Transform> transforms;
    private List<Vector3> rotations;

    // Use this for initialization
    void Start()
    {
        transforms = new List<Transform>();
        rotations = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            transform = hit.transform;
        }

        if (Input.GetMouseButtonDown(InteractionButton))
        {
            if (transform != null && transform.tag == "Mirror")
            {
                AddRotation(transform, new Vector3(0, 5, 0));
            } else if (transform != null && (transform.tag == "Turret" || transform.tag == "Turret-off"))
            {
                if (turretSource.tag == "Turret")
                {
                    turretSource.tag = "Turret-off";
                } else
                {
                    turretSource.tag = "Turret";
                }
            }
        } else if (Input.GetMouseButtonDown(SecondaryInteractionButton))
        {
            if (transform != null && transform.tag == "Mirror")
            {
                AddRotation(transform, new Vector3(0, -5, 0));
            }
        }

            RotateTransforms();
    }

    void RotateTransforms()
    {
        bool[] removes = new bool[transforms.Count];
        for (int i = 0; i < transforms.Count; i++)
        {
            Transform transform = transforms[i];
            Vector3 rotation = rotations[i];
            float maxRot = rotationSpeed * Time.deltaTime;
            float minRot = -rotationSpeed * Time.deltaTime;
            float xRot = rotation.x > maxRot ? maxRot : rotation.x;
            xRot = rotation.x < minRot ? minRot : xRot;
            float yRot = rotation.y > maxRot ? maxRot : rotation.y;
            yRot = rotation.y < minRot ? minRot : yRot;
            float zRot = rotation.z > maxRot ? maxRot : rotation.z;
            zRot = rotation.z < minRot ? minRot : zRot;
            if (xRot == 0 && yRot == 0 && zRot == 0)
            {
                removes[i] = true;
            }
            Vector3 rotVector = new Vector3(xRot, yRot, zRot);
            transform.Rotate(rotVector);
            rotations[i] = rotation - rotVector;
        }
        for (int i = 0; i < removes.Length; i++)
        {
            if (removes[i])
            {
                transforms.RemoveAt(i);
                rotations.RemoveAt(i);
            }
        }
    }

    void AddRotation(Transform transform, Vector3 rotation)
    {
        int index = transforms.IndexOf(transform);
        transforms.Add(transform);
        rotations.Add(rotation);
    }
}