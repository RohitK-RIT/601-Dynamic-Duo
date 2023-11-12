using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    public float fadeSpeed, fadeAmount;
    float originalOpacity;

    Renderer renderer;
    Material Mat;

    public bool DoFade = false;
    // Start is called before the first frame update
    void Start()
    {
        //Mat = GetComponent<Material>();
        renderer = GetComponent<Renderer>();
        Mat = renderer.material;
        originalOpacity = Mat.color.a;

    }

    // Update is called once per frame
    void Update()
    {
        if(DoFade)
        {
            FadeNow();
        }
        else
        {
            ResetFade();
        }
    }


    void FadeNow()
    {
        Color currentColor = Mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a,fadeAmount,fadeSpeed * Time.deltaTime));
        Mat.color = smoothColor;
    }


    void ResetFade()
    {
        Color currentColor = Mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
        Mat.color = smoothColor;
    }
}
