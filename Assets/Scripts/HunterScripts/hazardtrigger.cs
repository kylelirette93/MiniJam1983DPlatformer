using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class hazardtrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collisioninfo)
    {
        if (collisioninfo.gameObject.CompareTag("Player"))
        {
            // stop movement if colliding with another player.
            collisioninfo.gameObject.GetComponent<PlayerController>().StopMoving();

            Invoke("ChangeToGameOver", 2f);
        }


    }
    private void ChangeToGameOver()
    {
        // Change state to game over.
        GameManager.Instance.GameStateManager.SwitchToState(GameState.GameOver);
    }
}

