using Timer;

using Helpers.Shop.Model;

namespace Helpers
{
    public class ExtraTimeHelper : HelperBase
    {
        private CountdownTimer _timer;

        private float _timeAmount;

        public ExtraTimeHelper(GameData gameData, HelperView helperView, ShopModel shopModel, CountdownTimer timer, float timeAmount)
            : base(gameData, helperView, shopModel)
        {
            _timer = timer;

            _timeAmount = timeAmount;
        }

        protected override void UseHelper()
        {
            _timer.GiveExtraTime(_timeAmount);
        }
    }
}