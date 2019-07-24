using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportUI : MonoBehaviour
{
    [SerializeField] private Image[] TeleportImage = new Image[6];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.black;
            //gameObject.GetComponent<Image>().sprite = TeleportImage[0].sprite;
        } 
    }

   public void changeUI(string UIChange,int Tag)
    {
        switch (UIChange)
        {
            case "Nulled":
                gameObject.transform.GetChild(Tag).GetComponent<Image>().color = Color.black;
               // gameObject.GetComponent<Image>().sprite = TeleportImage[0].sprite;
                break;
            case "MarkedInRangeGround":
                gameObject.transform.GetChild(Tag).GetComponent<Image>().color = Color.blue;
                // gameObject.GetComponent<Image>().sprite = TeleportImage[1].sprite;
                break;
            case "MarkedOutOfRangeGround":
                gameObject.transform.GetChild(Tag).GetComponent<Image>().color = Color.red;
                //  gameObject.GetComponent<Image>().sprite = TeleportImage[2].sprite;
                break;
            case "MarkedInRangeSwitch":
                gameObject.transform.GetChild(Tag).GetComponent<Image>().color = Color.gray;
                //  gameObject.GetComponent<Image>().sprite = TeleportImage[3].sprite;
                break;
            case "MarkedOutOfRangeSwitch":
                gameObject.transform.GetChild(Tag).GetComponent<Image>().color = Color.yellow;
                //  gameObject.GetComponent<Image>().sprite = TeleportImage[4].sprite;
                break;
            case "MarkMoving":
                gameObject.transform.GetChild(Tag).GetComponent<Image>().color = Color.magenta;
                //  gameObject.GetComponent<Image>().sprite = TeleportImage[5].sprite;
                break;
            default:
                break;
        }
    }
}
