/*============================================================================== 
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/

using UnityEngine;
using System;
using Vuforia;

/// <summary>
/// This script sets up the background shader effect and contains the logic
/// to capture longer touch "drag" events that distort the video background. 
/// It also checks for OpenGL ES 2.0 support.
/// The background texture access sample does not support OpenGL ES 1.x
/// </summary>
[RequireComponent(typeof(GLErrorHandler))]
public class NegativeGrayscaleEffect : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES
    // time of last press down event
    private bool mErrorOccurred = false;
    private const string ERROR_TEXT = "The BackgroundTextureAccess sample requires OpenGL ES 2.0 or higher";
    private const string CHECK_STRING = "OpenGL ES";
    #endregion // PRIVATE_MEMBER_VARIABLES

	private float[] data;
	private Texture2D texture;

	void Start()
	{
		feed_data ();

		texture = new Texture2D (640, 480, TextureFormat.RGB24, true);

		Material mat = GetComponent<Renderer>().material;
		mat.SetTexture ("_Texture2", texture);

//		Color[] colors = new Color[3];
//		colors[0] = Color.red;
//		colors[1] = Color.green;
//		colors[2] = Color.blue;
//		int mipCount = Mathf.Min(3, tex.mipmapCount);
//
//		for( var mip = 0; mip < mipCount; ++mip ) {
//			var cols = tex.GetPixels( mip );
//			for( var i = 0; i < cols.Length; ++i ) {
//				cols[i] = Color.Lerp(cols[i], colors[mip], 0.33f);
//			}
//			tex.SetPixels( cols, mip );
//		}

		for (int i=0; i<640; i++) {
			for(int j=0; j<480; j++){
//				Color rgba;
//				rgba.r = (float)0.0;
//				rgba.g = (float)1.0;
//				rgba.b = (float)0.0;
//				rgba.a = (float)1.0;

//				Color color = ((i & j) != 0 ? Color.green : Color.red);

				Color color = Color.green;

				texture.SetPixel(i,j,color);
			}
		}
	

		texture.Apply ();

	}

	private void feed_data()
	{
		data = new float[640 * 480];
		for(int i = 0; i<640*480; i++)
		{
			data[i] = (float)i/640*480;
		}
	}


    public void InitEffect()
    {
        // This sample requires OpenGL ES 2.0 otherwise it won't work.
        mErrorOccurred = !IsOpenGLES2();

        if (mErrorOccurred)
        {
            Debug.LogError(ERROR_TEXT);

            // Show a dialog box with an error message.
            GLErrorHandler.SetError(ERROR_TEXT);

            // Turn off renderer to make sure the unsupported shader is not used.
            GetComponent<Renderer>().enabled = false;

            TrackableBehaviour[] tbs = (TrackableBehaviour[])FindObjectsOfType(typeof(TrackableBehaviour));
            if (tbs != null)
            {
                for (int i = 0; i < tbs.Length; ++i)
                {
                    tbs[i].enabled = false;
                }
            }
        }
    }
    
    public void UpdateEffect()
    {
        float touchX = 2.0F;
        float touchY = 2.0F;
        if(Input.GetMouseButton(0))
        {
            Vector2 touchPos = Input.mousePosition;
            // Adjust the touch point for the current orientation
            touchX = ((touchPos.x / Screen.width) - 0.5f)*2f;
            touchY = ((touchPos.y / Screen.height) - 0.5f)*2f;

        }

        Material mat = GetComponent<Renderer>().material;
        mat.SetFloat("_TouchX", touchX);
        mat.SetFloat("_TouchY", touchY);
    }
    
    #region PRIVATE_METHODS
    /// <summary>
    /// This method checks if we are using OpenGL ES 2.0 or later.
    /// </summary>
    private bool IsOpenGLES2()
    {
        // in play mode on a desktop machine, always return true
        if (VuforiaRuntimeUtilities.IsPlayMode()) return true;

        string graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;

        Debug.Log("Sample using " + graphicsDeviceVersion);

        int oglStringIdx = graphicsDeviceVersion.IndexOf(CHECK_STRING, StringComparison.Ordinal);
        if (oglStringIdx >= 0)
        {
            // it's open gl es, parse the version number
            float esVersion;
            if (float.TryParse(graphicsDeviceVersion.Substring(oglStringIdx + CHECK_STRING.Length + 1, 3), out esVersion))
            {
                return esVersion >= 2.0f;
            }
        }
        return false;
    }

    #endregion // PRIVATE_METHODS
}
