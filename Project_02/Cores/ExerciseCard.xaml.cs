using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_02.Cores
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExerciseCard : Grid
	{
		public ExerciseCard ()
		{
			InitializeComponent ();
		}
	}
}