using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportUI : MonoBehaviour
{
    [SerializeField] private Image[] m_fTeleportImage = new Image[6];
    [SerializeField]
    private int m_TeleporterMarkers = 3;
    [SerializeField]
    private Color[] Colors = new Color[6];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_TeleporterMarkers; i++)
        {
            gameObject.transform.GetChild(m_TeleporterMarkers + i).GetComponent<Image>().color = Color.black;
            //gameObject.GetComponent<Image>().sprite = m_fTeleportImage[0].sprite;
        }
    }

    public void changeUI(string _UIChange, int _Tag)
    {
        switch (_UIChange)
        {
            case "eNulled":
                gameObject.transform.GetChild(_Tag + m_TeleporterMarkers).GetComponent<Image>().color = Color.black;
                // gameObject.GetComponent<Image>().sprite = m_fTeleportImage[0].sprite;
                break;
            case "eMarkedInRangeGround":
                gameObject.transform.GetChild(_Tag + m_TeleporterMarkers).GetComponent<Image>().color = Color.blue;
                // gameObject.GetComponent<Image>().sprite = m_fTeleportImage[1].sprite;
                break;
            case "eMarkedOutOfRangeGround":
                gameObject.transform.GetChild(_Tag + m_TeleporterMarkers).GetComponent<Image>().color = Color.red;
                //  gameObject.GetComponent<Image>().sprite = m_fTeleportImage[2].sprite;
                break;
            case "eMarkedInRangeSwitch":
                gameObject.transform.GetChild(_Tag + m_TeleporterMarkers).GetComponent<Image>().color = Color.gray;
                //  gameObject.GetComponent<Image>().sprite = m_fTeleportImage[3].sprite;
                break;
            case "eMarkedOutOfRangeSwitch":
                gameObject.transform.GetChild(_Tag + m_TeleporterMarkers).GetComponent<Image>().color = Color.yellow;
                //  gameObject.GetComponent<Image>().sprite = m_fTeleportImage[4].sprite;
                break;
            case "eMarkMoving":
                gameObject.transform.GetChild(_Tag + m_TeleporterMarkers).GetComponent<Image>().color = Color.magenta;
                //  gameObject.GetComponent<Image>().sprite = m_fTeleportImage[5].sprite;
                break;
            default:
                break;
        }
    }
    public void SwitchingMarkers(int _highlightTag, int _NewTag)
    {
        Debug.Log(_highlightTag + " " + _NewTag);
        gameObject.transform.GetChild(_highlightTag).gameObject.SetActive(false);
        gameObject.transform.GetChild(_NewTag).gameObject.SetActive(true);
    }
    public Color GetColor(int _ColorLoctation){
        return Colors[_ColorLoctation];
    }
}
