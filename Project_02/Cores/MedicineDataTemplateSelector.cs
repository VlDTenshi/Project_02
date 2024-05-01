using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Project_02.Cores
{
    class MedicineDataTemplateSelector : DataTemplateSelector
    {
        public MedicineDataTemplateSelector()
        {

        }
        public DataTemplate Normal {  get; set; }
        public DataTemplate Special { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //var medicine = (Medicine)item;

            //return medicine.Type == "Type1" ? Special : Normal;

            //enable script above for true data template selectors

            return Normal;
        }
    }
}
