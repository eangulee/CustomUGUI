using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunctuationTextDemo : MonoBehaviour
{
    public Text txt;

    private string content;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (content != txt.text)
        {
            txt.PunctuationFormat();
            content = txt.text;
        }
    }
}