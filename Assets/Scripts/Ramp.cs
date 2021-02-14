using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{

    public GameObject cube1;
    public GameObject cube2;
    public GameObject ball1;
    public GameObject ball2;

    public float scale;
    private Transform local;

    // Start is called before the first frame update
    void Start()
    {
        // scale = cube1.gameObject.transform.localScale.x;
        Resize(scale);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Actual: " + ball1.gameObject.transform.position);
        // Debug.Log("Relative: " + (transform.position - ball1.gameObject.transform.position));
    }

    void FixedUpdate()
    {
        // Go to where the balls are
        Resize(scale);
    }

    public void Resize(float s) {
        Vector3 newscale = cube1.gameObject.transform.localScale;
        newscale.x = s;
        cube1.gameObject.transform.localScale = newscale;
        cube2.gameObject.transform.localScale = newscale;
        scale = s;
        float c = scale * 0.5F;
        float theta = 90f - Vector3.Angle(transform.up, new Vector3(1,0,0));
        Vector3 ballpos = new Vector3(0, 0, 0);
        float b = c * Mathf.Sin(Mathf.Deg2Rad * theta);
        float a = c * Mathf.Cos(Mathf.Deg2Rad * theta);
        ballpos.x = a;
        ballpos.y -= b;
        ball2.gameObject.transform.position = transform.position + ballpos;
        ball1.gameObject.transform.position = transform.position + (-ballpos);

    }
}
