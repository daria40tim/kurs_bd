using kurs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.ViewModel
{ 
    public class ChangeStageViewModel:DependencyObject
    {


        public int PlantCount
        {
            get { return (int)GetValue(PlantCountProperty); }
            set { SetValue(PlantCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlantCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlantCountProperty =
            DependencyProperty.Register("PlantCount", typeof(int), typeof(ChangeStageViewModel), new PropertyMetadata(null));



        public Plant SelectedPlant
        {
            get { return (Plant)GetValue(SelectedPlantProperty); }
            set { SetValue(SelectedPlantProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedPlant.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPlantProperty =
            DependencyProperty.Register("SelectedPlant", typeof(Plant), typeof(ChangeStageViewModel), new PropertyMetadata(null));



        public BaseCommand OKCommand
        {
            get { return (BaseCommand)GetValue(OKCommandProperty); }
            set { SetValue(OKCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OKCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OKCommandProperty =
            DependencyProperty.Register("OKCommand", typeof(BaseCommand), typeof(ChangeStageViewModel), new PropertyMetadata(null));


        static public event Action<bool> OnClose;

        public ChangeStageViewModel(Plant selectedPlant)
        {
            InitializeCommads();
            SelectedPlant = selectedPlant;
            Collection.Plants.Where(o => o.Plant_id == selectedPlant.Plant_id).FirstOrDefault().Count = Collection.Plants.Where(o => o.Plant_id == selectedPlant.Plant_id).FirstOrDefault().Count - PlantCount;
            MessageBox.Show(selectedPlant.Count.ToString());
        }

        private void InitializeCommads()
        {
            OKCommand = new BaseCommand(OKMethod);
        }

        private void OKMethod()
        {
            OnClose(true);
        }

    }
}
