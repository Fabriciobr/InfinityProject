﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : EnemyState {

	float interp;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy = animator.GetComponent<Enemy> ();
		enemy.sightColor = Color.red;
		animator.SetBool ("PlayerIsDetected", true);
		animator.SetBool ("PlayerIsDead", false);
		animator.SetBool ("PlayerIsLost", false);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (enemy.PlayerIsSeen () || (enemy.IsHit && enemy.player != null))
			Chase ();
		else if (enemy.PlayerIsHeard ())
			LookAtPlayer ();
		else if (enemy.player != null) {
			animator.SetBool ("PlayerIsDetected", false);
		} else {
			animator.SetBool ("PlayerIsDead", true);
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}

	private void Chase()
	{
		Vector3 direction = enemy.player.position - enemy.transform.position;

		if(direction != Vector3.zero){
			enemy.transform.rotation = Quaternion.LookRotation(direction);
		}
			
		if (direction.magnitude > enemy.specs.attackRange*.5f) {
			//too far
			enemy.Move (direction);
		} 

		if (direction.magnitude <= enemy.specs.attackRange){ 
			//close enough to attack
			enemy.Shoot();
		}
	}

	private void LookAtPlayer()
	{
		Quaternion start = enemy.transform.rotation;
		Quaternion finish = enemy.transform.rotation;

		// cancel height to avoid loking strainght up if player is on my head
		Vector3 lookat = enemy.player.transform.position - enemy.transform.position;
		lookat.y = 0;

		finish.SetLookRotation (lookat);

		enemy.transform.rotation = Quaternion.Lerp (start, finish, interp);

		interp += enemy.specs.chasingTurningSpeed * Time.deltaTime;
	}
}
