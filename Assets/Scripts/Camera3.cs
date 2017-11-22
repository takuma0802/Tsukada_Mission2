using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Camera3 : MonoBehaviour, ICamera {
	
	private Tween _tween;
	private GameObject _target;
	private Vector3 _lookAtPos;
	private Vector3 _offSet;
	private Vector3 _startPos;
	private Quaternion _startRot;

	void Awake()
	{
		_lookAtPos = new Vector3(0, 0, 0);
		_offSet = new Vector3(0, 3.5f, 0f);
		_startPos = transform.position;
		_startRot = transform.rotation;
	}

	private void LateUpdate()
	{
		this.transform.LookAt(_lookAtPos);
	}
	
	public void CameraMove(GameObject target)
	{
		print("Camera3");
		_target = target;
		_lookAtPos = _target.transform.position + _offSet;
		
		_tween = transform.DOLocalPath(new Vector3[] {_startPos, new Vector3(4f, 10f, 0), new Vector3(0f, 1f, 10f)},
				15f, PathType.CatmullRom)
			.SetEase(Ease.OutCubic)
			.SetLoops(-1, LoopType.Yoyo);
	}
	
	public void CameraPause()
	{
		_tween.Kill();
		transform.position = _startPos;
		transform.rotation = _startRot;
	}
}
