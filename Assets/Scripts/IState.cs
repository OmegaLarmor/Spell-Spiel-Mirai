using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState {

	void Enter();
	void Execute(); //ie Update
	void Exit();

}
