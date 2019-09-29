using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DontDestoryRecords : MonoBehaviour
{
    //note first one is not a speed run mode so 
    static private float[] gamemodeBestSpeed = new float[6] {0,0,0,0,0,0 };
    static private int[] gamemodeBesttrophie = new int[6] {0,0,0,0,0,0 };
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("TimeRecords");
        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    public void SetNewSpeedMod(int _mode,float _time,int _trophie)
    {
        if (gamemodeBestSpeed[_mode] != 0)
        {
            Debug.Log("just checking");
            if (gamemodeBestSpeed[_mode] >= _time)
            {
                gamemodeBestSpeed[_mode] = _time;
                gamemodeBesttrophie[_mode] = _trophie;
                Debug.Log("newsave");
            }
        }
        else
        {
            Debug.Log("newsaved");

            Debug.Log("saved At: "+ _mode);
            Debug.Log("time of At: " + _time);
            Debug.Log("teophie is: " + _trophie);
            gamemodeBestSpeed[_mode] = _time;
            gamemodeBesttrophie[_mode] = _trophie;

        }
    }
    public void GetSpeedMod(int _mode, out float _time, out int _trophie)
    {
        _time = gamemodeBestSpeed[_mode];
        _trophie = gamemodeBesttrophie[_mode];
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        Debug.Log("best speed: "+gamemodeBestSpeed[1]);
    //        Debug.Log("best trophie: " + gamemodeBesttrophie[1]);
    //    }
    //}
}