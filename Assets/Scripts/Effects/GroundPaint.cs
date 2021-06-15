using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPaint : MonoBehaviour
{
    private static Vector3 OFFSET = new Vector3(0f, .01f, 0f);

    [SerializeField]
    private GameObject effect;
    [SerializeField]
    private float delay = 1f;
    [SerializeField]
    private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(delay);
        RaycastHit hit;
        Ray ray = new Ray()
        {
            origin = transform.position,
            direction = Vector3.down
        };

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Instantiate(effect, hit.point + OFFSET, effect.transform.rotation);
        }
    }
}
