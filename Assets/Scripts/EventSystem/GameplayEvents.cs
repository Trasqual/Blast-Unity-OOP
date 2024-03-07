using BlastGame.BoardItems.Data;
using BlastGame.BoardManagement;
using BlastGame.GoalSystem;
using UnityEngine;

namespace BlastGame.EventSystem
{
    public struct ClickEvent
    {
        public Vector3 Position;
    }

    public struct BoardIsReadyEvent
    {
        public CentralizedVerticalBoard Board;
    }

    public struct CameraIsScaledEvent
    {
        public Camera Cam;
    }

    public struct ItemCollectedEvent
    {
        public ItemCollectionData ItemCollectionData;
    }

    public struct GoalItemBeingProcessedEvent
    {
        public ItemCollectionData ItemCollectionData;
    }

    public struct AllGoalsAreCompletedEvent
    {

    }

    public struct LegalMoveMadeEvent
    {

    }

    public struct MoveProcessedEvent
    {
        public int ProcessedMoves;
    }

    public struct OutOfMovesEvent
    {

    }

    public struct GameLoopHasStartedEvent
    {

    }

    public struct BlockGameLoopEvent
    {

    }

    public struct ReleaseGameLoopEvent
    {

    }

    public struct GameLoopHasEndedEvent
    {

    }
}
