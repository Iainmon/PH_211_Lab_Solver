using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject floor;
    public GameObject ramp;
    public GameObject sceneController;
    public bool mainball = false;

    private SceneController sc;

    // Start is called before the first frame update
    void Start()
    {
        sc = sceneController.GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "floor") {
            sc.HitFloor(this);
        }
    }

    void FixedUpdate() {
        if (transform.position.y < -75f) {
            if (mainball) {
                GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                return;
            }
            Destroy(gameObject);
        }
    }
}
