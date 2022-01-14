using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImgHover : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    GameObject text;
    GameObject uiManagement;

    void Start()
    {
        uiManagement = GameObject.Find("UIManagement");
        if (transform.Find("Text")!=null)
            text = transform.Find("Text").gameObject;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (text!=null)
            text.SetActive(true);
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (text!=null)
            text.SetActive(false);
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        float x=0, y=0;
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        foreach(Image img in uiManagement.GetComponent<UIController>().Imgs)
        {
            if(img.transform.position.x==x||
            img.transform.position.y==y)
            {
                img.gameObject.SetActive(false);
                //uiManagement.GetComponent<UIController>().ImgsPosition.Remove(img.transform.position);
            }
        }
        uiManagement.GetComponent<UIController>().Root.Remove(gameObject.GetComponent<Image>());
        //Debug.Log(gameObject.GetComponent<Image>().transform.position);
        uiManagement.SendMessage("MoveImg");
        
    }
}
