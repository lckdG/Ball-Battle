﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    [SerializeField] private LayerMask field_layer;
    private bool cast = true;

    private void Start()
    {
        GameManager.GetInstance().RegisterCameraRaycaster(this);
    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (!cast) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit info;
            if (Physics.Raycast(ray, out info, 100, field_layer))
            {
                info.collider.GetComponent<Field>().Spawn(info.point);
            }
        }

#elif UNITY_ANDROID || UNITY_IOS
        
        if (Input.touchCount > 0)
        {            
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit info;
                if (Physics.Raycast(ray, out info, 100, field_layer))
                {
                    info.collider.GetComponent<Field>().Spawn(info.point);
                }
            }
        }       
#endif
    }

    public void EnableCasting() => cast = true;
    public void DisableCasting() => cast = false;

}
