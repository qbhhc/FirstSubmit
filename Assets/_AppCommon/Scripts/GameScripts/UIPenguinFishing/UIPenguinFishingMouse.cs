﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIPenguinFishingMouse : UIBaseInit
{
    private Image myImage;
    private Tween myTween;
    private Vector3[] myMousePath;
    private bool firstCollide = true;
    void Awake()
    {
        myImage = this.GetComponent<Image>();
    }

    void OnTriggerEnter2D(Collider2D e)
    {        
        if (e.transform.name == "WordLadder")
        {
            UIPenguinFishing.Instance.myMouseTween.Kill();
            myImage.sprite = GetS("UIPenguinFishing_MouseD");
            myImage.SetNativeSize();
            myTween = this.transform.DOPath(UIPenguinFishing.Instance.ladderPath, 2f);
        }
        if (e.transform.name == "DownStone")
        {
            myTween.Kill();
            myImage.sprite = GetS("UIPenguinFishing_MouseLR");
            myImage.SetNativeSize();
            UIPenguinFishing.Instance.myRMouse.eulerAngles = Vector3.zero;
            myMousePath = new Vector3[2] { this.transform.position, UIPenguinFishing.Instance.Cake.position };
            myTween = this.transform.DOPath(myMousePath, (Vector3.Distance(myMousePath[0],myMousePath[1]))/5f);            
        }
        if (e.transform.name == "Cake")
        {
            UIPenguinFishing.Instance.DecideStar(true);
        }
        if (e.transform.name == "AskLadderRight")
        {
            UIPenguinFishing.Instance.myMouseTween.Kill();
            myImage.sprite = GetS("UIPenguinFishing_MouseD");
            myImage.SetNativeSize();
            myTween = this.transform.DOPath(UIPenguinFishing.Instance.RightLadderPath, 1f);
        }
        if (e.transform.name == "AskLadderLeft")
        {
            UIPenguinFishing.Instance.myMouseTween.Kill();
            myImage.sprite = GetS("UIPenguinFishing_MouseD");
            myImage.SetNativeSize();
            myTween = this.transform.DOPath(UIPenguinFishing.Instance.LeftLadderPath, 1f);
        }
        if (e.transform.parent != null)
        {
            if (e.transform.parent.name == "AskStonePosDown")
            {                
                if (firstCollide)
                {
                    if (myTween != null)
                    {
                        myTween.Kill();
                    }
                    myImage.sprite = GetS("UIPenguinFishing_MouseLR");
                    myImage.SetNativeSize();
                    UIPenguinFishing.Instance.UpTureDownFlase = false;
                    Vector3 tempVec = UIPenguinFishing.Instance.AskStonePosDown.GetChild(UIPenguinFishing.Instance.AskStonePosDown.childCount - 1).position;
                    tempVec.y += 0.12f;
                    myMousePath = new Vector3[2] { this.transform.position, tempVec };
                    myTween = this.transform.DOPath(myMousePath, (Vector3.Distance(myMousePath[0], myMousePath[1])) / 4f);
                    myTween.onComplete += () =>
                    {
                        UIPenguinFishing.Instance.AskCreatePath(false);
                        UIPenguinFishing.Instance.AskMouseMoveRefresh();
                        firstCollide = false;
                    };
                }

            }
        }
        
        
    }
}
