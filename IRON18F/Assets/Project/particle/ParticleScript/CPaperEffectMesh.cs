using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CPaperEffectMesh : MonoBehaviour
{

    class ParticleInfo
    {
        public float elapsedTime;
        public float spinSpeed;

        public Vector3 velocity;

        public float rotateSpeed;
        public float rotateRadius;
        public float size;

        public Color color;

        public ParticleInfo(float particleSize, float fallSpeed)
        {
            spinSpeed = 30.0f;
            elapsedTime = Random.Range(0.0f, 2.0f);

            float speedRadius = Mathf.Sqrt(Random.Range(0.0f, 0.2f));
            float speedTheta = Random.Range(0.0f, Mathf.PI * 2);

            velocity = new Vector3(speedRadius * Mathf.Cos(speedTheta), -fallSpeed, speedRadius * Mathf.Sin(speedTheta));

            size = particleSize;
            rotateRadius = 0.1f;
            rotateSpeed = Random.Range(0, 1) == 0 ? 5.0f : -5.0f;

            color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        }

        public void Tick()
        {
            elapsedTime += Time.deltaTime;
        }

        public Vector3[] Vertices(out Vector3 normal)
        {
            Vector3[] retval = new Vector3[4];

            Vector3 center = velocity * elapsedTime;

            float pivotAngle = elapsedTime * rotateSpeed;

            Vector3 pivot = new Vector3(Mathf.Cos(pivotAngle), 0.0f, Mathf.Sin(pivotAngle));

            center += (pivot * rotateRadius);

            Vector3 extent = pivot * size;
            Quaternion rot = Quaternion.AngleAxis(spinSpeed * Mathf.Rad2Deg * elapsedTime, pivot);

            Vector3 baseCoExtent = Vector3.up * size;
            Vector3 coExtent = rot * baseCoExtent;

            for (int i = 0; i < 4; ++i)
            {
                retval[i] = center + extent * (i / 2) + coExtent * (i % 2);
            }

            normal = Vector3.Cross(pivot, coExtent).normalized;

            return retval;
        }
    }

    public int count = 30;
    public float particleRadius = 0.1f;
    public float fallSpeed = 0.5f;

    ParticleInfo[] _particles;
    Mesh _mesh;


    Vector3[] _vertices;
    int[] _indices;
    Color[] _colors;
    Vector3[] _normals;

    void Setup(int particleCount)
    {
        _particles = new ParticleInfo[particleCount];

        for (int i = 0; i < particleCount; ++i)
        {
            _particles[i] = new ParticleInfo(particleRadius, fallSpeed);
        }

        int vertexCount = particleCount * 4;

        _mesh = new Mesh();// GetComponent<MeshFilter>().sharedMesh;

        _vertices = new Vector3[vertexCount];
        _indices = new int[particleCount * 6];
        _colors = new Color[vertexCount];
        _normals = new Vector3[vertexCount];

        // vertices : ??
        // normals : ??
        // indices
        for (int i = 0; i < particleCount; ++i)
        {
            int vtxBase = i * 4;
            int idxBase = i * 6;

            _indices[idxBase + 0] = vtxBase + 0;
            _indices[idxBase + 1] = vtxBase + 1;
            _indices[idxBase + 2] = vtxBase + 2;
            _indices[idxBase + 3] = vtxBase + 3;
            _indices[idxBase + 4] = vtxBase + 2;
            _indices[idxBase + 5] = vtxBase + 1;
        }

        // colors
        for (int i = 0; i < particleCount; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                _colors[i * 4 + j] = _particles[i].color;
            }
        }

        _mesh.vertices = _vertices;
        _mesh.SetIndices(_indices, MeshTopology.Triangles, 0);
        _mesh.normals = _normals;
        _mesh.colors = _colors;

        GetComponent<MeshFilter>().sharedMesh = _mesh;
    }

    // Use this for initialization
    void Start()
    {
        Setup(count);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ParticleInfo p in _particles)
        {
            p.Tick();
        }

        for (int i = 0; i < _particles.Length; ++i)
        {
            Vector3[] pos;
            Vector3 normal;

            pos = _particles[i].Vertices(out normal);

            for (int j = 0; j < 4; ++j)
            {
                _vertices[i * 4 + j] = pos[j];
                _normals[i * 4 + j] = normal;
            }
        }

        _mesh.vertices = _vertices;
        _mesh.normals = _normals;
        _mesh.RecalculateBounds();
    }
}