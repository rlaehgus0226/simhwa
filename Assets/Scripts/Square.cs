using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Square : MonoBehaviour
{
    private Vector3 initialScale;
    private Vector3 maxScale;
    private Renderer obj;

    private void Start()
    {
        maxScale = new Vector3(3.0f, 3.0f, 3.0f);
        initialScale = new Vector3(0.1f, 0.1f, 0.1f);
        obj = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (transform.localScale == maxScale)
        {
            ResetScale();
        }
    }

    private void ResetScale()
    {
        transform.localScale = initialScale;
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        obj.material.color = randomColor;
    }
}