using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class CustomCameraScript : MonoBehaviour {

	public static CustomCameraScript instance;

	public Material webCamShow;    //1.普通的材质球渲染方式
	
	public string deviceName;
	
	public WebCamTexture customTexture;

//    private Vector2 resSize = new Vector2(1280, 740);
	private Vector2 resSize = new Vector2(960, 1080);
//    private Vector2 resSize = new Vector2(640, 480);
	
	public int cameraIndex;

	public GameObject ARCamera;

	public GameObject rrr;

	void Awake()
	{
		instance = this;
	}

	Material mat;
	void Start()
	{
		mat = rrr.GetComponent<Renderer>().material;
		StartCoroutine (webCam ());
	}

	void Update () 
	{
		if(ARCamera!=null)
		{
			this.GetComponent<Camera>().projectionMatrix = Camera.main.projectionMatrix;
		}
	}


	public IEnumerator webCam()
	{
		
		yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
		
		if (Application.HasUserAuthorization(UserAuthorization.WebCam))  
		{  
			WebCamDevice[] devices = WebCamTexture.devices;  

			deviceName = devices[cameraIndex].name; 

			customTexture= new WebCamTexture(deviceName, (int)resSize.x, (int)resSize.y, 30); 

			//webCamShow.mainTexture = customTexture;    //属于1方法的内容
			//mat.mainTexture = customTexture;

			//customTexture.Play();

			//zxw
//			PTAM_AR_script.instance.webcam_texture_right = customTexture;
//			Debug.Log("PTAM_AR_script.instance.webcam_texture_righ - " + PTAM_AR_script.instance.webcam_texture_right.GetPixels32().Length);
		}  
	}

	void qqq()
	{
		Debug.Log("qqq");
		if(customTexture)
			Debug.Log("yep");
		else
			Debug.Log("no");
	}


	void OnApplicationQuit()
	{
		Debug.Log("customTexture quit");
		customTexture.Stop ();
	}


 
}
