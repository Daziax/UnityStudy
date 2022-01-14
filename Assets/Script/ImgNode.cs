using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class ImgNode
{
    Image img;
    int level;
    public Image Img => img;
    public int Level => level;
    //int level;
    ImgNode child;
    //bool isRoot;
    Dictionary<float,ImgNode> children;
    public ImgNode(int level, Image img = null)
    {
        //this.isRoot = isRoot;
        if (level>0)
        {
            //this.img = img.transform.position;
            if(img!=null)
                this.img = img;
            this.level = level;
        }
        else
        {
            children = new Dictionary<float, ImgNode>(15);
        }
    }
    public void Add(Image img)
    {
        ImgNode tempChild;
        if (!children.ContainsKey(img.transform.position.x))
        {
            children.Add(img.transform.position.x, new ImgNode(level + 1, img));
        }
        else
        {
            tempChild = children[img.transform.position.x];
            children[img.transform.position.x] = new ImgNode( tempChild.level + 1, img);
            children[img.transform.position.x].child = tempChild;
        }
        //if (isRoot)
        //{
        //    if (!children.ContainsKey(vector.x))
        //        children.Add(vector.x, new ImgNode(img, level + 1));
        //    else
        //        Add(img);
        //}
        //else
        //{
        //    if (child == null)
        //        child = new ImgNode(img, level + 1);
        //    else
        //    {
        //        tempChild = child;
        //        child= new ImgNode(img, level + 1);
        //        child.child = tempChild;
        //    }
        //}
    }
    public void Remove(Image img)
    {
        int removedNodeLevel = Find(img).level;
        children.Remove(img.transform.position.x);
        ImgNode curNode;
        ImgNode tempNode;
        foreach(KeyValuePair<float,ImgNode> node in children)
        {
            curNode = node.Value;
            while (curNode!=null)
            {
                if(curNode.child?.level==removedNodeLevel)
                {
                    tempNode = curNode.child;
                    curNode.child = tempNode.child;
                }
                if(curNode.level<removedNodeLevel)
                {
                    ++curNode.level;
                }
                curNode = curNode.child;
            }
        }
    }
    public ImgNode Find(Image img)
    {
        ImgNode child = children[img.transform.position.x];
        while(child!=null)
        {
            if(img.transform.position==child.img.transform.position)
            {
                return child;
            }
            child = child.child;
        }
        return null;
    }

    public List<ImgNode> AllNode()
    {
        List<ImgNode> imgNodes=new List<ImgNode> (15);
        ImgNode curNode;
        foreach (KeyValuePair<float, ImgNode> node in children)
        {
            curNode = node.Value;
            while (curNode != null)
            {
                imgNodes.Add(curNode);
                curNode = curNode.child;
            }
        }
        return imgNodes;
    }
}

