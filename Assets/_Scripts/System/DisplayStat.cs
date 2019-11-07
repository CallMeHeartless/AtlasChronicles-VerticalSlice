using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class DisplayStat : MonoBehaviour
{
    [SerializeField] GameObject m_rCollectableText;
    [SerializeField] GameObject m_rMapCountText;
    [SerializeField] Sprite[] m_rMapImages;
    [SerializeField] GameObject m_rCurrentMapImage;

    [SerializeField] GameObject[] m_rHearts;
    [SerializeField] GameObject m_rHeart;
    [SerializeField] GameObject m_rUIGamePanel;
    [SerializeField] PlayableDirector[] m_rDirectors;

    private GameObject[] m_rMapReferences;
    private bool m_bShown = false;
    public int m_iHP = 4;

    // Start is called before the first frame update
    void Start()
    {
        //viv-----
        UpdateHealth(m_iHP);

        HideUIGamePanel(true);

        //Find all collectables placed in the level
        GameStats.s_iCollectableTotal[GameStats.s_iLevelIndex] = GameObject.FindGameObjectsWithTag("SecondaryPickup").Length;

        //Find all the references for the map fragments in the level
        m_rMapReferences = GameObject.FindGameObjectsWithTag("PrimaryPickup");

        //Assign the amount of map fragments in the level to GameStats
        GameStats.s_iMapsTotal[GameStats.s_iLevelIndex] = m_rMapReferences.Length;
        
        //Find all treasure chests in the level and count the number of secondary pickups that are contained in them
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Box");
        for(int i = 0; i < chests.Length; ++ i)
        {
            GameObject[] prizes = chests[i].GetComponent<BreakableObject>().GetPrizes();
            for (int j = 0; j < prizes.Length; ++j)
            {
                if(prizes[j].CompareTag("SecondaryPickup"))
                {
                    ++GameStats.s_iCollectableTotal[GameStats.s_iLevelIndex];
                }
            }
        }
        //Enable text for counters
        m_rCollectableText.SetActive(true);
        m_rMapCountText.SetActive(true);
    }

    // Update is called once per frame
    void Update() { // Viv
        //Update text based on how many collectables have been collected
        m_rCollectableText.GetComponent<TextMeshProUGUI>().text = 
            GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex].ToString() 
            + "/" + GameStats.s_iCollectableTotal[GameStats.s_iLevelIndex];

        int numMaps = GameStats.s_iMapsBoard[GameStats.s_iLevelIndex];
        //Update text based on how many maps have been collected
        m_rMapCountText.GetComponent<TextMeshProUGUI>().text =
            numMaps.ToString() 
            + "/" + GameStats.s_iMapsTotal[GameStats.s_iLevelIndex];

        m_rCurrentMapImage.GetComponent<Image>().sprite = m_rMapImages[numMaps];
    }
  
    ////NIK
    //public void NewHealth(int HP) {
    //    for (int i = 0; i < m_rHearts.Length; i++) {
    //        if (HP<= i) {
    //            m_rHearts[i].SetActive(false);
    //        }
    //        else {
    //            m_rHearts[i].SetActive(true);
    //        }
    //    }
    //    UpdateHealth(HP); // Viv // Testing quarter heart update
    //}

    /******************************************************************
     * UpdateHealth: Updates fill of the heart in quarter decrements
     * Author: Vivian
     ******************************************************************/
    public void UpdateHealth(int _hp) {
        //Hide and Show UI Gamepanel IF maps do not exist when taking damage
        if (GameStats.s_iMapsBoard[GameStats.s_iLevelIndex] > 0) {
            HideUIGamePanel(false);
        }

        //Update health in four parts //Coded for radial fill but works for other modes too
        if (m_rHeart) {
            m_rHeart.GetComponent<Image>().fillAmount = _hp * 90.0f / 360.0f;
        }
    }

    /******************************************************************
     * HideUIGamePanel: Hides UI Game Panel via PlayableDirectors/Timeline animations
     * Author: Vivian
     ******************************************************************/
    public void HideUIGamePanel(bool _hide) {
        //Invoke method to hide UI game panel in 3 seconds
        if (_hide) {
            //Cancel all previous invokes to prevent awkward looking transitionss
            CancelInvoke();
            Invoke("HideGameUIPanel", 3.0f);
        }
        else {
            //Instantly show UI game panel if it is not already being shown
            if (!m_bShown) {
                m_bShown = true;
                m_rDirectors[1].Play();
            }
        }
    }

    /******************************************************************
     * HideGameUIPanel: Hides game panel after 3 seconds and marks boolean as not shown
     * Author: Vivian
     ******************************************************************/
    void HideGameUIPanel() {

        if(!m_bShown)
        {
            return;
        }

        if (!GetDirectorIsPlaying(0)) {
            m_rDirectors[0].Play();
            m_bShown = false;
        }
    }

    /******************************************************************
     * GetDirectorIsPlaying: Check if director specified is playing
     * Author: Vivian
     ******************************************************************/
    bool GetDirectorIsPlaying(int _director) {
        //Check if director specified is playing
        if (m_rDirectors[_director].playableGraph.IsValid()) {
            return m_rDirectors[_director].playableGraph.IsPlaying();
        }
        return false;
    }
}
