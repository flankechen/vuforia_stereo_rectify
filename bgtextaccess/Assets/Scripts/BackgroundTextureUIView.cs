/*============================================================================== 
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;
using Vuforia;

public class BackgroundTextureUIView : ISampleAppUIView {
    
    #region PUBLIC_PROPERTIES
    public CameraDevice.FocusMode FocusMode
    {
        get {
            return mFocusMode;
        }
        set {
            mFocusMode = value;
        }
    }
    #endregion PUBLIC_PROPERTIES
    
    #region PUBLIC_MEMBER_VARIABLES
    public event System.Action TappedToClose;
    public SampleAppUIBox mBox;
    public SampleAppUILabel mBackgroundTextureLabel;
    public SampleAppUICheckButton mAboutLabel;
    public SampleAppUIButton mCloseButton;
    #endregion PUBLIC_MEMBER_VARIABLES
    
    #region PRIVATE_MEMBER_VARIABLES
    private CameraDevice.FocusMode mFocusMode;
    private SampleAppsUILayout mLayout;
    #endregion PRIVATE_MEMBER_VARIABLES
    
    #region PUBLIC_METHODS
    
    public void LoadView()
    {
        mLayout = new SampleAppsUILayout();
        mBackgroundTextureLabel = mLayout.AddLabel("Background Texture");
        mAboutLabel = mLayout.AddSimpleButton("About");
        mLayout.AddGap(2);

        Rect CloseButtonRect = new Rect(0, Screen.height - (100 * Screen.width) / 800.0f, Screen.width, (70.0f * Screen.width) / 800.0f);
        mCloseButton = mLayout.AddButton("Close", CloseButtonRect);
    }
    
    public void UnLoadView()
    {
        mBackgroundTextureLabel = null;
        mAboutLabel = null;
    }
    
    public void UpdateUI(bool tf)
    {
        if(!tf)
        {
            return;
        }
        mLayout.Draw();
    }

    public void OnTappedToClose ()
    {
        if(this.TappedToClose != null)
        {
            this.TappedToClose();
        }
    }
    #endregion PUBLIC_METHODS
}

