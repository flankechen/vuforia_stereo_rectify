using UnityEngine;
using System.Collections;

public class PlaneAutoScale : MonoBehaviour
{
	public GameObject targetMesh;
	
	void Update () 
	{

		if(targetMesh.GetComponent<MeshFilter>())
		{
				this.transform.localPosition = targetMesh.transform.localPosition;
				this.transform.localEulerAngles = targetMesh.transform.localEulerAngles;
				this.transform.localScale = new Vector3 (targetMesh.transform.localScale.x, targetMesh.transform.localScale.y, targetMesh.transform.localScale.z);
                
				Mesh targetmesh = targetMesh.GetComponent<MeshFilter>().mesh;
				this.GetComponent<MeshFilter>().mesh = targetmesh;

		}
	}
}
