using UnityEngine;

public class SoftBody : MonoBehaviour
{

    public GameObject rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            // instantiate a rigid body at the position of each vertex
            GameObject rb = new GameObject();
            rb.AddComponent<Rigidbody>();
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = transform.TransformPoint(vertices[i]);
            sphere.transform.localScale = Vector3.one * 0.08f;

            ConfigurableJoint joint = rb.AddComponent<ConfigurableJoint>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
