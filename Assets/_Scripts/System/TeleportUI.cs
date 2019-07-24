using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportUI : MonoBehaviour
{
    [SerializeField] private Image[] m_fTeleportImage = new Image[6];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.black;
            //gameObject.GetComponent<Image>().sprite = m_fTeleportImage[0].sprite;
        }
    }

   public void changeUI(string _UIChange,int _Tag)
    {
        switch (_UIChange)
        {
            case "eNulled":
                gameObject.transform.GetChild(_Tag).GetComponent<Image>().color = Color.black;
                // gameObject.GetComponent<Image>().sprite = m_fTeleportImage[0].sprite;
                break;
            case "eMarkedInRangeGround":
                gameObject.transform.GetChild(_Tag).GetComponent<Image>().color = Color.blue;
                // gameObject.GetComponent<Image>().sprite = m_fTeleportImage[1].sprite;
                break;
            case "eMarkedOutOfRangeGround":
                gameObject.transform.GetChild(_Tag).GetComponent<Image>().color = Color.red;
                //  gameObject.GetComponent<Image>().sprite = m_fTeleportImage[2].sprite;
                break;
            case "eMarkedInRangeSwitch":
                gameObject.transform.GetChild(_Tag).GetComponent<Image>().color = Color.gray;
                //  gameObject.GetComponent<Image>().sprite = m_fTeleportImage[3].sprite;
                break;
            case "eMarkedOutOfRangeSwitch":
                gameObject.transform.GetChild(_Tag).GetComponent<Image>().color = Color.yellow;
                //  gameObject.GetComponent<Image>().sprite = m_fTeleportImage[4].sprite;
                break;
            case "eMarkMoving":
                gameObject.transform.GetChild(_Tag).GetComponent<Image>().color = Color.magenta;
                //  gameObject.GetComponent<Image>().sprite = m_fTeleportImage[5].sprite;
                break;
            default:
                break;
        }
    }
}
