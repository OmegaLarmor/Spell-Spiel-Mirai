using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState {

	void Enter();
	IState Execute(); //ie Update
	void Exit();
	void ChangeTurn();
	void SetParentMachine(StateMachine machine);

}
