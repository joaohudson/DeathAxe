using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Singleton
    public static CameraController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField]
    private Transform follow;

    private Vector3 offset;
    private Vector3 vibration = Vector3.zero;
    private float vibrationFactor = 0f;
    private float magnitude = 0f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - follow.position;
    }

    private void Update()
    {
        if(vibrationFactor > 0f)
        {
            vibrationFactor -= Time.deltaTime * 4f;
            float x = Mathf.Sin(vibrationFactor * 20f) * magnitude;
            vibration = new Vector3(x, 0f, 0f);
        }
    }

    private void LateUpdate()
    {
        transform.position = offset + follow.position + vibration;
    }

    public void Vibrate(float magnitude)
    {
        this.magnitude = magnitude;
        vibrationFactor = 1f;
    }
}
