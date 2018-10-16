using kurs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.ViewModel
{
    public class CardsViewModel:DependencyObject
    {
        public CardsViewModel(Plant SelectedPlant)
        {
            Collection.FillData();
           if (SelectedPlant == null)
            {
                PlantCardCollection = Collection.Plants.OrderBy(o=>o.Plant_id).First().Cards_of_plant;
            }
            else
            {
              PlantCardCollection = SelectedPlant.Cards_of_plant;
            }
        }

        public List<Card> PlantCardCollection
        {
            get { return (List<Card>)GetValue(PlantCardCollectionProperty); }
            set { SetValue(PlantCardCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlantCardCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlantCardCollectionProperty =
            DependencyProperty.Register("PlantCardCollection", typeof(List<Card>), typeof(CardsViewModel), new PropertyMetadata(null));


    }
}
