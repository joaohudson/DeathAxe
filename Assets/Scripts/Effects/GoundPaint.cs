using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundPaint : MonoBehaviour
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


        if(Physics.Raycast(transform.position, Vector3.down, out hit, groundLayer))
        {
            Instantiate(effect, hit.point + OFFSET, effect.transform.rotation);
        }
    }
}
