using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumerBlockController : MonoBehaviour
{
    public Block block;
    TextMesh text;

    void Start()
    {
        GetComponent<Renderer>().sortingLayerName = "Text";
        text = GetComponent<TextMesh>();
        text.text = block.randomLife.ToString();
        text.color = block.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        text.text = block.randomLife.ToString();
        text.color = block.GetComponent<SpriteRenderer>().color;
    }
}
