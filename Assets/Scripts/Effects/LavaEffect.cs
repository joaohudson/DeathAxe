using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LavaEffect : MonoBehaviour
{
    [SerializeField]
    private Vector2 waveDirection = Vector2.right;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private Color emission = Color.yellow;
    [SerializeField]
    private float emissionSpeed = 2f;

    private MaterialPropertyBlock prop;
    private MeshRenderer meshRenderer;
    
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        prop = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = waveDirection * Time.time * speed;
        Vector4 st = new Vector4(64, 64, offset.x, offset.y);
        prop.SetVector("_MainTex_ST", st);

        var intensity = Mathf.Sin(Time.time *  emissionSpeed) * .3f + 1f;
        Color currentEmission = emission * intensity;
        prop.SetColor("_EmissionColor", currentEmission);

        meshRenderer.SetPropertyBlock(prop);
    }
}
