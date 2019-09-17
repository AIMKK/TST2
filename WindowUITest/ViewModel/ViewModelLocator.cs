/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WinTest"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
//using Microsoft.Practices.ServiceLocation;

namespace WindowUITest
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<LoginUIViewModel>();

            SimpleIoc.Default.Register<MainUIViewModel>();

            SimpleIoc.Default.Register<LoginLocationSelectUIViewModel>();
        }
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        //
        public LoginUIViewModel LoginUIViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginUIViewModel>();
            }
        }

        //
        public MainUIViewModel MainUIViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainUIViewModel>();
            }
        }
        //
        public LoginLocationSelectUIViewModel LoginLocationSelectUIViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginLocationSelectUIViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels            
        }
    }
}