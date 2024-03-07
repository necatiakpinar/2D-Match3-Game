using NecatiAkpinar.Enums;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Miscs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NecatiAkpinar.UI.Widgets
{
    public class GoalWidget : MonoBehaviour
    {
        [SerializeField] private Image _goalImage;
        [SerializeField] private TMP_Text _goalAmountLabel;

        private GoalType _goalType;
        private int _goalAmount;

        public GoalType GoalType => _goalType;

        public void Init(GoalType goalType, Sprite goalSprite, int goalAmount)
        {
            _goalType = goalType;
            _goalImage.sprite = goalSprite;
            _goalAmount = goalAmount;
            _goalAmountLabel.text = _goalAmount.ToString();
        }

        public void UpdateAmount()
        {
            if (_goalAmount > 0)
            {
                GFXManager.Instance.PlayVFX(Constants.VFX_GoalWidgetCollection, transform.position, Quaternion.identity);
                _goalAmount--;
            }

            if (_goalAmount <= 0)
            {
                _goalAmountLabel.text = "0";
            }
            else
                _goalAmountLabel.text = _goalAmount.ToString();
        }
    }
}