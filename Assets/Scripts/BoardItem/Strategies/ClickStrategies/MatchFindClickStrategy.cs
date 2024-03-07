using BlastGame.BoardItems.Core;
using BlastGame.BoardManagement.Tiles;
using BlastGame.MatchFindingSystem;
using BlastGame.MatchManagement;
using BlastGame.ServiceManagement;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.BoardItems.Strategies.ClickStrategies
{
    [CreateAssetMenu(fileName = "MatchFindClickStrategy", menuName = "Strategies/ItemClickStrategy/MatchFindClickStrategy")]
    public class MatchFindClickStrategy : ItemClickStrategy
    {
        public override void Execute(BoardItem item)
        {
            List<Tile> matches = ServiceLocator.Instance.GetService<IMatchFinder>().FindMatches(item.CurrentTile);

            ServiceLocator.Instance.GetService<IMatchManager>().ProcessMatchedTiles(item.CurrentTile, matches);
        }
    }
}
