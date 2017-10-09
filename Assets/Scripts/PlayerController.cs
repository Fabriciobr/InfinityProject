﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Living {

	[SerializeField]
	private float _gravity = 20.0F;

	private Vector3 _moveDirection = Vector3.zero;
	[SerializeField]
	private CharacterController _controller;

	void Start(){
		_controller = GetComponent<CharacterController>();
	}

	void Update() 
	{
		Vector3 LookAt = new Vector3(Input.GetAxisRaw("RHorizontal"), 0, -Input.GetAxisRaw("RVertical"));
		if (LookAt != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(LookAt);

		if (_controller.isGrounded) 
		{
			_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			//moveDirection = transform.TransformDirection(moveDirection);
			_moveDirection *= _speed;
		}

		_moveDirection.y -= _gravity * Time.deltaTime;
		_controller.Move(_moveDirection * Time.deltaTime);
		
	}
}
