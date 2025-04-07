using UnityEngine;

namespace LudumDare57.Game
{
    public class PlayerActor : MonoBehaviour
    {
        public void Kick() => GameContext.Player.KickOutBackpack();
        public void PlayAnim() => GameContext.Player.Animator.SetTrigger("end");
    }
}