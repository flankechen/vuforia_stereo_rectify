using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using Vuforia;


[RequireComponent(typeof(GLErrorHandler))]
public class BackgroundRectifyRight : MonoBehaviour {
	
	[DllImport("opencv_dll", EntryPoint="create_opencv_wrapper")]
	public static extern IntPtr create_opencv_wrapper();
	
	[DllImport("opencv_dll", EntryPoint="get_map_x1")]
	public static extern IntPtr get_map_x1(IntPtr opencv_wrapper_ptr);
	
	[DllImport("opencv_dll", EntryPoint="get_map_y1")]
	public static extern IntPtr get_map_y1(IntPtr opencv_wrapper_ptr);
	
	[DllImport("opencv_dll", EntryPoint="get_map_x2")]
	public static extern IntPtr get_map_x2(IntPtr opencv_wrapper_ptr);
	
	[DllImport("opencv_dll", EntryPoint="get_map_y2")]
	public static extern IntPtr get_map_y2(IntPtr opencv_wrapper_ptr);
	
	#region PRIVATE_MEMBER_VARIABLES
	// time of last press down event
	private bool mErrorOccurred = false;
	private const string ERROR_TEXT = "The BackgroundTextureAccess sample requires OpenGL ES 2.0 or higher";
	private const string CHECK_STRING = "OpenGL ES";
	#endregion // PRIVATE_MEMBER_VARIABLES
	
	private int width = 960;
	private int height = 1080;
	
	private Texture2D texture;
	private float[] map_x2;
	private float[] map_y2;
	
	IntPtr opencv_wrapper_ptr;
	
	// Use this for initialization
	void Start () {

		
		texture = new Texture2D (width, height, TextureFormat.RGFloat, false);
		
		WebCamTexture customTexture= new WebCamTexture(CustomCameraScript.instance.deviceName, width, height, 30);  
		Material mat = GetComponent<Renderer>().material;
		mat.mainTexture = customTexture;
		mat.SetTexture ("_Texture2", texture);
		customTexture.Play ();
//		
		opencv_wrapper_ptr = create_opencv_wrapper ();
//		
		map_x2 = new float[width * height];
		map_y2 = new float[width * height];
//		
		IntPtr ptr =  get_map_x2 (opencv_wrapper_ptr);
		Marshal.Copy (ptr, map_x2, 0, width * height);
		
		ptr = get_map_y2 (opencv_wrapper_ptr);
		Marshal.Copy (ptr, map_y2, 0, width * height);
		
		Debug.Log("map x2 0: "+map_x2[1000]);
		Debug.Log("map x2 1: "+map_x2[1001]);
		Debug.Log("map x2 2: "+map_x2[2]);
		Debug.Log("map x2 3: "+map_x2[3]);
		Debug.Log("map y2 0: "+map_y2[1000]);
		Debug.Log("map y2 1: "+map_y2[1001]);
		Debug.Log("map y2 2: "+map_y2[2]);
		Debug.Log("map y2 3: "+map_y2[3]);
//		
//		
		for (int i=0; i<width; ++i) {
			for(int j=0; j<height; ++j){
				Color color;
				color.r = map_x2[j*width+i]/(float)width;
				color.g = map_y2[j*width+i]/(float)height;
//				color.r = ((float)i-0.00f)/(float)width;
//				color.g = ((float)j-0.0f)/(float)height;
				color.b = 0.0f;
				color.a = 1.0f;
				texture.SetPixel(i,j,color);
			}
		}
		
		texture.Apply ();
	}


	// Update is called once per frame
	void Update () {

	}
}
