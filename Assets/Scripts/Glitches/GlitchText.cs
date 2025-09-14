using UnityEngine;
using TMPro;

public class GlitchText : MonoBehaviour
{
    public TMP_Text tmpText;
    public float intensity = 5f;
    public float speed = 30f;
    private Mesh mesh;
    private Vector3[] vertices;

    void Awake()
    {
        if (tmpText == null)
            tmpText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        tmpText.ForceMeshUpdate();
        mesh = tmpText.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            float glitch = Mathf.PerlinNoise(Time.time * speed, i * 0.1f);
            vertices[i] += new Vector3((glitch - 0.5f) * intensity, (glitch - 0.5f) * intensity, 0);
        }

        mesh.vertices = vertices;
        tmpText.canvasRenderer.SetMesh(mesh);
    }
}
