using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{

    public GameObject ramp;
    public GameObject textBox;
    public GameObject heightIndicator;
    public GameObject heightIndicatorText;
    public GameObject widthIndicator;
    public GameObject widthIndicatorText;
    public GameObject ball;
    public GameObject statusIndicator;

    private float startTime;
    private GameObject[] balls;
    private TextMeshPro txtmp;
    private TextMeshPro heightText;
    private TextMeshPro widthText;
    private Vector3 startingPosition;
    private Stats stats;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        
        balls = new GameObject[2];
        balls[0] = ramp.transform.GetChild(2).gameObject;
        balls[1] = ramp.transform.GetChild(3).gameObject;

        txtmp = textBox.GetComponent<TextMeshPro>();
        txtmp.text = "Falling...";

        heightText = heightIndicatorText.GetComponent<TextMeshPro>();
        widthText = widthIndicatorText.GetComponent<TextMeshPro>();

        ball.GetComponent<Ball>().mainball = true;
        startingPosition = new Vector3(0,0,0);
        startingPosition += ball.gameObject.transform.position;

        // for (int i = 0; i < 200; i++) {
        //     float x = Random.Range(-10, 10);
        //     float y = Random.Range(-10, 10);
        //     Vector3 pos = new Vector3(x, 10, y);
        //     GameObject newball = Instantiate(ball.gameObject);
        //     newball.transform.position = pos;
        //     newball.GetComponent<Ball>().mainball = false;
        // }

        stats = statusIndicator.GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform hpole = heightIndicator.gameObject.transform;
        Vector3 position;
        Vector3 scale;

        float opposite = 0f;
        float adjacent = 0f;

        position = balls[0].transform.position;
        scale = heightIndicator.gameObject.transform.localScale;
        scale.y = position.y / 2f;
        position.y -= scale.y;
        hpole.position = position;
        hpole.localScale = scale;
        heightText.text = "h = " + Round(position.y, 2) + "m";
        Vector3 htextPosition = position;
        htextPosition.x -= 3f;
        heightIndicatorText.gameObject.transform.position = htextPosition;

        Transform wpole = widthIndicator.gameObject.transform;
        scale = widthIndicator.gameObject.transform.localScale;
        Vector3 diff = balls[1].transform.position - position; diff.x = Mathf.Abs(diff.x);

        scale.y = diff.x / 2f;
        position.x += scale.y;
        position.y = Mathf.Min(balls[0].transform.position.y, balls[1].transform.position.y);
        wpole.position = position;
        wpole.localScale = scale;
        widthText.text = "w = " + Round(diff.x, 2) + "m";
        Vector3 wtextPosition = position;
        wtextPosition.y -= 2f;
        widthIndicatorText.gameObject.transform.position = wtextPosition;
        
        stats.Clear();
        opposite = Mathf.Abs(balls[0].transform.position.y - balls[1].transform.position.y);
        adjacent = Mathf.Abs(balls[0].transform.position.x - balls[1].transform.position.x);
        if (adjacent == 0f) { adjacent = 0.00001F; }
        float theta = Mathf.Atan(opposite / adjacent);
        stats.AddLine(Round(theta, 4) + "rad = θ");
        stats.AddLine(Round(Mathf.Rad2Deg * theta, 2) + "° = θ");
    }

    void Respawn() {
        GameObject go = Instantiate(ball.gameObject);
        go.transform.position = startingPosition;
        go.GetComponent<Ball>().mainball = false;
        startTime = Time.time;
    }


    public void HitFloor(Ball ball) {
        float dt = Time.time - startTime;
        txtmp.text = "" + Round(dt, 2) + "s";
        Respawn();
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }
}
