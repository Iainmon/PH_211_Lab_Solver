using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{

    private TextMeshPro txtmp;

    // Start is called before the first frame update
    void Start()
    {
        txtmp = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLine(string str) {
        txtmp.text += "\n" + str;
    }
    public void Clear() {
        txtmp.text = "";
    }
}
