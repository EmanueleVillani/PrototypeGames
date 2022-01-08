using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CheckImage : MonoBehaviour
{
    Image img;
    AspectRatioFitter fitter;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        fitter = GetComponent<AspectRatioFitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fitter.aspectRatio != img.preferredWidth / img.preferredHeight)
        {
            fitter.aspectRatio = img.preferredWidth / img.preferredHeight;
        }
    }
}
