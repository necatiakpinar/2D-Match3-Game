using System;
using NecatiAkpinar.Enums;
using UnityEngine;


namespace NecatiAkpinar.Managers
{
    public static class EventManager
    {
        public static Action OnTileActivated;
        public static Action<bool> OnLevelFinished;
        public static Action<GoalType> OnGoalUpdatedUI;
        public static Action<GoalType> OnGoalUpdated;
        public static Action OnMovesUpdated;
        public static Action<bool> OnLevelFinishedUI;

        public static Func<GoalType, Vector3> GetGoalWidgetPosition;
        public static Func<GoalType, bool> IsGoalAvailable; 
    }
}