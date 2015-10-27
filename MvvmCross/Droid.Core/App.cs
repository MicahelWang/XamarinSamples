using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;

namespace Droid.Core
{
    public class App: MvxApplication
    {
        public override void Initialize()
        {

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<ViewModels.FirstViewModel>();

        }
    }
}