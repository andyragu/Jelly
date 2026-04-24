using System.Collections.Generic;
using UnityEngine;

public class SoftBody : MonoBehaviour
{

    public GameObject rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        Rigidbody[] rigidBodies = new Rigidbody[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            // instantiate a rigid body at the position of each vertex
            GameObject rb = new GameObject();
            rb.AddComponent<Rigidbody>();

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = transform.TransformPoint(vertices[i]);
            sphere.transform.localScale = Vector3.one * 0.08f;

            rigidBodies[i] = rb.GetComponent<Rigidbody>();
        }

        HashSet<string> edges = new HashSet<string>();
    }

    void addEdge(int a, int b, HashSet<string> edges, Rigidbody[] bodies)
    {
        int min = Mathf.Min(a, b);
        int max = Mathf.Max(a, b);

        string edgeKey = min + "-" + max;

        if (!edges.Contains(edgeKey)) 
            edges.Add(edgeKey);

        Rigidbody rbA = bodies[a];
        Rigidbody rbB = bodies[b];

        ConfigurableJoint joint = rbA.gameObject.AddComponent<ConfigurableJoint>();
        joint.connectedBody = rbB;

        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Limited;
        joint.zMotion = ConfigurableJointMotion.Limited;
    }

}
