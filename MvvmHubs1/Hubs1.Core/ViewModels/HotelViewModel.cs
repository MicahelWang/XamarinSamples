namespace Hubs1.Core.ViewModels
{
    public class HotelViewModel : BaseViewModel
    {
        private HotelDataModel _hotelData;

        public HotelDataModel HotelData
        {
            get
            {
                return _hotelData;
            }

            set
            {
                _hotelData = value;
                RaisePropertyChanged(() => HotelData);
            }
        }

        public void Init(HotelDataModel hotelData)
        {
            HotelData = hotelData;
        }
    }
}