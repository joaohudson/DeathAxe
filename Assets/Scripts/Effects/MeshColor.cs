using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MeshColor : MonoBehaviour
{
    [SerializeField]
    private Color color = Color.white;

    private MaterialPropertyBlock prop;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        prop = new MaterialPropertyBlock();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        prop.SetColor("_Color", color);
        meshRenderer.SetPropertyBlock(prop);
    }
}
