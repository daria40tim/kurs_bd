using kurs.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.ViewModel
{
    public class DataGridViewModel:DependencyObject
    {
        public DataGridViewModel()
        {
            Collection.FillData();
            PlantCollection = Collection.Plants;
        }

        public ObservableCollection<Plant> PlantCollection
        {
            get { return (ObservableCollection<Plant>)GetValue(PlantCollectionProperty); }
            set { SetValue(PlantCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlantCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlantCollectionProperty =
            DependencyProperty.Register("PlantCollection", typeof(ObservableCollection<Plant>), typeof(DataGridViewModel), new PropertyMetadata(null));


        public Plant SelectedPlant
        {
            get { return (Plant)GetValue(SelectedPlantProperty); }
            set { SetValue(SelectedPlantProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedPlant.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPlantProperty =
            DependencyProperty.Register("SelectedPlant", typeof(Plant), typeof(DataGridViewModel), new PropertyMetadata(null));

    }
}
