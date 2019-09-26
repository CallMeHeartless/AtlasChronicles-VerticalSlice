﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject selectedObject;

    private bool bButtonSelected;
    private AudioSource m_Next;


    // Use this for initialization
    void Start ()
    {
        eventSystem = FindObjectOfType<EventSystem>();

        OnEnable();

        //eventSystem.SetSelectedGameObject(null);
        //eventSystem.SetSelectedGameObject(selectedObject);
        bButtonSelected = true;

        m_Next = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (eventSystem == null || selectedObject == null)
        {
            return;
        }

        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !bButtonSelected)
        {
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(selectedObject);
            bButtonSelected = true;

            if (null != m_Next)
                m_Next.Play();
        }

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) 
          || Input.GetMouseButtonDown(2)) 
          && eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(selectedObject);
            selectedObject.GetComponent<Button>().Select();
        }
        print("Selected object = " + selectedObject.name);


        if (Input.GetKeyDown("joystick 1 button 1"))
        {
            print("X PRESSED DLKFHJDKLSFJKSDJGHKLJDGKLJHDFGJKHDFGKJHDFKLJGKLJH");
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(selectedObject);
            selectedObject.GetComponent<Button>().Select();
        }
    }

    private void OnDisable()
    {
    }

    private void OnEnable()
    {
        if(eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
        }
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(selectedObject);
        selectedObject.GetComponent<Button>().Select();
        bButtonSelected = true;
    }
}
