using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEditor;

public class Camera1 : MonoBehaviour, ICamera
{
	private Tween _tween;
	private GameObject _target;
	private Vector3 _startPos;
	private Vector3 _lookAtPos;
	private Vector3 _offSet;
	private Quaternion _startRot;

	void Awake()
	{
		_lookAtPos = new Vector3(0, 0, 0);
		_offSet = new Vector3(0, 3.5f, 0f);
		_startPos = transform.position;
		_startRot = transform.rotation;

		// 初回再生時のみ
		_tween = transform.DOLocalPath(
				new Vector3[] {_startPos, _lookAtPos + _offSet, new Vector3(0f, 10f, 13f)},
				20f, PathType.CatmullRom)
			.SetEase(Ease.OutCubic)
			.SetLoops(-1, LoopType.Yoyo);
	}

	private void LateUpdate()
	{
		this.transform.LookAt(_lookAtPos);
	}
	
	public void CameraMove(GameObject target)
	{
		print("Camera1");
		_target = target;
		_lookAtPos = _target.transform.position + _offSet;
		
		Vector3 targetPos = _target.transform.position + _offSet + new Vector3(0, 0, 2f);
		_tween = transform.DOLocalPath(new Vector3[] {_startPos, targetPos, new Vector3(0f, 10f, 13f)},
				20f, PathType.CatmullRom)
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
