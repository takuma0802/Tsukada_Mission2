using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;


public class CameraController : MonoBehaviour
{
	[SerializeField] private GameObject _target;
	private Camera[] _cameras;
	private GameObject _activeCamera;
	
	// Use this for initialization
	void Start ()
	{
		_cameras = this.GetComponentsInChildren<Camera>();
		_activeCamera = _cameras[0].gameObject;
		for (int i = 1; i < _cameras.Length; i++) _cameras[i].gameObject.SetActive(false);
		
		// controlボタン
		this.UpdateAsObservable()
			.Where(x => Input.GetButtonDown("Fire1"))
			.Subscribe(x => CameraChange(0));
		// shiftボタン
		this.UpdateAsObservable()
			.Where(x => Input.GetButtonDown("Fire3"))
			.Subscribe(x => CameraChange(1));
		// optionボタン
		this.UpdateAsObservable()
			.Where(x => Input.GetButtonDown("Fire2"))
			.Subscribe(x => CameraChange(2));
	}

	void CameraChange(int cameraNum)
	{
		_activeCamera.SetActive(false);
		_activeCamera.GetComponent<ICamera>().CameraPause();

		_cameras[cameraNum].gameObject.SetActive(true);
		_cameras[cameraNum].GetComponent<ICamera>().CameraMove(_target);
		_activeCamera = _cameras[cameraNum].gameObject;
	}
}
