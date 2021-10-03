using UnityEngine;
using System.Collections;

public class RotateSample : MonoBehaviour
{	
	void Start(){
		iTween.RotateBy(gameObject, iTween.Hash("y", 1, "easeType", iTween.EaseType.linear, "loopType", "loop", "delay", 0, "time", 5));
	}
}

