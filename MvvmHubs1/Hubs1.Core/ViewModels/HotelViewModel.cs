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

        public string TxtBtnIos6 => "IOS6 Style";
        public string TxtBtnIos7 => "IOS7 Style";

        public void Init(HotelDataModel hotelData)
        {
            HotelData = hotelData;
        }

        //public ICommand ShowIos6StyleCommand
        //{
        //    get
        //    {
        //        return new MvxCommand(() =>
        //  {


        //  });
        //    }
        //}

        //public ICommand ShowIos7StyleCommand
        //{
        //    get { return new MvxCommand(() => ShowViewModel<HotelViewModel>(this)); }
        //}
    }
}