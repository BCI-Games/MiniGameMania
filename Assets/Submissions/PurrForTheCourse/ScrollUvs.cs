using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUvs : MonoBehaviour
{
    private float uvx = 0.0f;
    public float scrollSpeed = 1.0f;
    private float toSet = 0.0f;

    public Renderer renderer;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = renderer.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        toSet += scrollSpeed * Time.deltaTime;
        mat.SetTextureOffset("_MainTex", new Vector2(toSet, 0.0f));
    }
}
