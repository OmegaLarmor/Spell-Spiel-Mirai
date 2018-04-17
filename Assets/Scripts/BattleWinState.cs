using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleWinState : IState
{

    private StateMachine machine;

    public void Enter()
    {

        BattleController.instance.player.animator.SetBool("Casting", false);

        BattleController.instance.progress.variable.value++;

        BattleController.instance.battleText.text = "やった！" + BattleController.instance.player.name + "がかちぬけた！";
        BattleController.instance.battleText.GetComponent<Flicker>().enabled = true;

        BattleController.instance.StartCoroutine(WinFireworks());
		BattleController.instance.enemy.GetComponent<SpriteRenderer>().enabled = false;


    }

    public void Exit()
    {

        BattleController.instance.battleText.GetComponent<Flicker>().enabled = false;

        BattleController.instance.StopCoroutine(WinFireworks());

    }

    public IState Execute()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {

            if (BattleController.instance.progress.variable.value > BattleController.instance.progress.maxValue)
            {
                SceneManager.LoadScene("EndSequence");
            }

            else SceneManager.LoadScene("BattleScene");
        }

        return null;

    }

    public void SetParentMachine(StateMachine machine)
    {
        this.machine = machine;
    }

    //It's in the interface for technical reasons
    public void ChangeTurn()
    {

    }

    private IEnumerator WinFireworks()
    {

        while (true)
        {
            float x = Random.Range(-4.15f, 4.15f);
            float y = Random.Range(1.3f, 3.15f);

            GameObject.Instantiate(BattleController.instance.perfectPoof, new Vector3(x, y, 0), Quaternion.identity);

            yield return new WaitForSeconds(1);
        }
    }

}
