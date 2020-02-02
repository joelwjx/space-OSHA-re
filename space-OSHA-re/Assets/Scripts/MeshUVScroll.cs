using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MeshUVScroll : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.mainTextureOffset += scrollSpeed * Time.deltaTime;
    }
}
