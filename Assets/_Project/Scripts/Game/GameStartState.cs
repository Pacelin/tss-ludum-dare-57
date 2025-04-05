using System.Threading;
using Cysharp.Threading.Tasks;
using LudumDare57.Hole;
using TSS.ContentManagement;
using UnityEngine;

namespace LudumDare57.Game
{
    public class GameStartState : GameState
    {
        public override UniTask Start(CancellationToken cancellationToken)
        {
            GameContext.CancellationToken = cancellationToken;
            GameContext.Hole = new HoleModel(0, CMS.Hole.Config.InitialHoleSpeed);
            GameContext.HoleEntries = new HoleEntries();
            GameContext.HoleEntries.Initialize();
            GameContext.HoleView = Object.Instantiate(CMS.Hole.Prefab);
            GameContext.PlayerView = Object.Instantiate(CMS.Player.Prefab);
            NextTo(new GameLiftDownState());
            return UniTask.CompletedTask;
        }
        
        public override void Update() { }
    }
}