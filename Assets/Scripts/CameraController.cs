using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;


public class CameraController : MonoBehaviour
{
	private Camera[] _cameras;
	private int _activeCameraNum = 0;
	private bool _isAnim = false;
	
	// Use this for initialization
	void Start ()
	{
		_cameras = this.GetComponentsInChildren<Camera>();
		for (int i = 1; i < _cameras.Length; i++) _cameras[i].gameObject.SetActive(false);
		
		this.UpdateAsObservable()
			.Where(x => Input.GetMouseButtonDown(0) & !_isAnim)
			.Subscribe(x => CameraChange());
	}

	void CameraChange()
	{
		_activeCameraNum = (_activeCameraNum < _cameras.Length - 1) ? _activeCameraNum + 1 : 0;	
		for (int i = 0; i < _cameras.Length; i++)
		{
			_cameras[i].gameObject.SetActive(false);
			if (i == _activeCameraNum) _cameras[i].gameObject.SetActive(true);
		}
	}
}
