using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

class UIController : MonoBehaviour
{
    GameObject test;
    GameObject canvas;
    Image[] imgs;
    //List<Vector2> vector2;
    ImgNode root ;
    bool isClick;
    List<ImgNode> allNodes;
    float[] levels ;
    private void Awake()
    {
        levels = new float[4] { 351.0f,251.0f,151f,51f}; //{ 150f, 50f, -50f, -150f };
        isClick = false;
        canvas = GameObject.Find("Canvas");
        imgs = canvas.GetComponentsInChildren<Image>();
        //vector2 = new List<Vector2>(15);
        root = new ImgNode(0);
        int index = 2;
        Text text;

        foreach (Image img in imgs)
        {
            img.gameObject.AddComponent<ImgHover>();
            //vector2.Add(img.transform.position);
            root.Add(img);
            text = img.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = index.ToString();
                text.gameObject.SetActive(false);
            }
            index += 2;
        }
    }
    private void Update()
    {
        if (isClick)
        {
            foreach (ImgNode node in allNodes)
            {
                node.Img.transform.position = Vector2.MoveTowards(node.Img.transform.position, new Vector2(node.Img.transform.position.x, levels[node.Level - 1]), 40 * Time.deltaTime);
            }
        }
    }
    public void MoveImg()
    {
        allNodes = root.AllNode();
        isClick = true;
        
    }
    //public List<Vector2> ImgsPosition => vector2;
    public ImgNode Root => root;

    public Image[] Imgs
    {
        get => imgs;
        set => imgs = value;
    }
}

