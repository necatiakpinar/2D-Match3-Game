using System;
using NecatiAkpinar.Enums;
using UnityEngine;

namespace NecatiAkpinar.Level
{
    [Serializable]
    public class GameGoal
    {
        [SerializeField] private GoalType _type;
        [SerializeField] private int _amount;

        public GoalType Type => _type;
        public int Amount => _amount;
    }
}