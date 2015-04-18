using UnityEngine;
using System.Collections;

public class scrWindStreams : MonoBehaviour 
{
	int pointCount = 2;
	float pathLength = 20;
	float pointDeviation =3f;
	Vector3[] path = null;
	Vector3 rootPosition;
	
	void Start()
	{
		GenerateRandomPath();
		iTween.MoveTo(gameObject,iTween.Hash("path",path,"time",3,"easetype",iTween.EaseType.easeInOutCubic, "oncomplete", "Remove"));		
	}

	void Remove()
	{
		Destroy(gameObject);
	}
	
	void GenerateRandomPath()
	{
		rootPosition = transform.position;
		path = new Vector3[pointCount+2];
		float pointGap = pathLength/pointCount;
		path[0]=rootPosition;
		path[pointCount+1]=new Vector3(rootPosition.x,rootPosition.y,rootPosition.z-(pathLength+pointGap));
		for (int i = 1; i < pointCount+1; i++) {
			float randomX = rootPosition.x + Random.Range(-pointDeviation,pointDeviation);
			float randomY = rootPosition.y + Random.Range(-pointDeviation,pointDeviation);
			float newZ = rootPosition.z - (pointGap*i);
			path[i]=new Vector3(randomX,randomY,newZ);
		}
	}
}
