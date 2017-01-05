using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace DragAndDropListSample
{
	public partial class MainPage : ContentPage
	{
		
		public ObservableCollection<CellItem> Items1 { get; set;}
		public ObservableCollection<CellItem> Items2 { get; set;}
		public ObservableCollection<CellItem> Items3 { get; set;}
		public ObservableCollection<CellItem> Items4 { get; set;}

		public MainPage ()
		{
			Items1 = new ObservableCollection<CellItem>(new List<CellItem> () {
				new CellItem(){Name="Test1", Height=30.0},
				new CellItem(){Name="Test2", Height=60.0},
				new CellItem(){Name="Test3", Height=20.0},
			});
			Items2 = new ObservableCollection<CellItem>(new List<CellItem> () {
				new CellItem(){Name="SecondTest1", Height=80.0},
				new CellItem(){Name="SecondTest2", Height=80.0},
				new CellItem(){Name="SecondTest3", Height=40.0}
			});

			Items3 = new ObservableCollection<CellItem>(new List<CellItem> () {
				new CellItem(){Name="Test5", Height=30.0},
				new CellItem(){Name="Test6", Height=20.0},
				new CellItem(){Name="Test7", Height=120.0},
			});
			Items4 = new ObservableCollection<CellItem>(new List<CellItem> () {
				new CellItem(){Name="SecondTest4", Height=100.0},
				new CellItem(){Name="SecondTest5", Height=70.0},
				new CellItem(){Name="SecondTest6", Height=60.0}
			});

			InitializeComponent();
			this.BindingContext = this;
		}
	}

	public class CellItem
	{
		public string Name { set; get; }
		public double Height { set; get; }
	}
}

