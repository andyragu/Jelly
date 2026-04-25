using UnityEngine;

public class Jellifier : MonoBehaviour
{
    public float bounceSpeed;
    public float fallForce;

    public float stiffness;

    private MeshFilter meshFilter;

    private Mesh mesh;

    Vector3[] initialVertices;
    Vector3[] currentVertices;

    Vector3[] vertexVelocities;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;

        initialVertices = mesh.vertices;

        currentVertices = new Vector3[initialVertices.Length];
        vertexVelocities = new Vector3[initialVertices.Length];

        for (int i = 0; i < initialVertices.Length; i++)
        {
            currentVertices[i] = initialVertices[i];
        }
    }

    private void Update()
    {
        UpdateVerticies();
    }

    private void UpdateVerticies()
    {
        for (int i = 0; i < initialVertices.Length; i++)
        {
            Vector3 currDisplacement = currentVertices[i] - initialVertices[i];
            vertexVelocities[i] -= currDisplacement * bounceSpeed * Time.deltaTime;

            vertexVelocities[i] *= 1f - stiffness * Time.deltaTime;
            currentVertices[i] += vertexVelocities[i] * Time.deltaTime;
        }

        mesh.vertices = currentVertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    public void OnCollisionEnter(Collision other)
    {
        ContactPoint[] collisonPoints = other.contacts;
        for (int i = 0; i < collisonPoints.Length; i++)
        {
            Vector3 inputPoint = collisonPoints[i].point + (collisonPoints[i].point * .1f);
            ApplyPressureToPoint(inputPoint, fallForce);
        }
    }

    public void ApplyPressureToPoint(Vector3 _point, float _pressure)
    {
        for (int i = 0; i < currentVertices.Length; i++)
        {
            ApplyPressureToVertex(i, _point, _pressure);
        }
    }

    public void ApplyPressureToVertex(int i, Vector3 _point, float _pressure)
    {
        Vector3 distanceVerticePoint = currentVertices[i] - transform.InverseTransformPoint(_point);

        float adaptedPressure = _pressure / (1f + distanceVerticePoint.sqrMagnitude);

        float velocity = adaptedPressure * Time.deltaTime;

        vertexVelocities[i] += distanceVerticePoint.normalized * velocity;
    }
}
