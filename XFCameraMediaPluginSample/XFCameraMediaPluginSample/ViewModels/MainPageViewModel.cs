using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace XFCameraMediaPluginSample.ViewModels
{
    class MainPageViewModel : BindableBase
    {
        private bool isBusy;

        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.SetProperty(ref this.isBusy, value); }
        }
    }
}
