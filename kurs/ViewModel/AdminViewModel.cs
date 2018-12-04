using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.ViewModel
{
   public  class AdminViewModel:DependencyObject
    {
        public AdminViewModel()
        {
            CropVM = new CropViewModel();
        }


        public CropViewModel CropVM
        {
            get { return (CropViewModel)GetValue(CropVMProperty); }
            set { SetValue(CropVMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CropVM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CropVMProperty =
            DependencyProperty.Register("CropVM", typeof(CropViewModel), typeof(AdminViewModel), new PropertyMetadata(null));


    }
}
