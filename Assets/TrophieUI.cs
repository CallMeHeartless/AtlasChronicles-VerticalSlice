using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrophieUI : MonoBehaviour
{
    public Sprite[] m_spSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   public void setSprite(int _spSprite)
    {
       GetComponent<Image>().sprite = m_spSprite[_spSprite];
    }
}
