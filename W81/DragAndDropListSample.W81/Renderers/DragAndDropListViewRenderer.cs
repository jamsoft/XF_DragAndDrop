using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragAndDropListSample.W81;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using System.Collections;
using Windows.UI.Xaml;
using DragAndDropListSample;
using Windows.UI.Xaml.Controls;
using Windows.UI.Input;
using System.ComponentModel;
using Windows.ApplicationModel.DataTransfer;
using Xamarin.Forms.Platform.WinRT;


[assembly: ExportRenderer(typeof(DraggableListView), typeof(DragAndDropListViewRenderer))]
namespace DragAndDropListSample.W81
{
    public class DragAndDropListViewRenderer : ListViewRenderer
    {
        const string DragItemKey = "DragAndDropListViewRenderer.DraggedItem";
        const string SourceItemsKey = "DragAndDropListViewRenderer.SourceItems";

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {

            base.OnElementChanged(e);

            var listView = Control as Windows.UI.Xaml.Controls.ListView;

            if (e.NewElement != null)
            {
                listView.AllowDrop = true;
                listView.CanDragItems = true;

                listView.DragItemsStarting += ListView_DragItemsStarting;
                listView.DragOver += Control_DragOver;
                listView.DragLeave += Control_DragLeave;
                listView.DragEnter += Control_DragEnter;
                listView.Drop += Control_Drop;               
              
#if WINDOWS_PHONE_APP
                listView.ReorderMode = ListViewReorderMode.Enabled;
#else
                listView.CanReorderItems = true;
#endif
            }

            if (e.OldElement != null)
            {
                if (listView != null)
                {
                    listView.DragItemsStarting -= ListView_DragItemsStarting;
                    listView.DragOver -= Control_DragOver;
                    listView.DragLeave -= Control_DragLeave;
                    listView.DragEnter -= Control_DragEnter;
                    listView.Drop -= Control_Drop;
                }
            }
        }

        private void ListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            e.Data.Properties.Add(DragItemKey, e.Items.FirstOrDefault());
            e.Data.Properties.Add(SourceItemsKey, Element.ItemsSource);
        }


        private void Control_DragOver(object sender, Windows.UI.Xaml.DragEventArgs e)
        {

        }

        private void Control_DragLeave(object sender, Windows.UI.Xaml.DragEventArgs e)
        {


        }

        private void Control_DragEnter(object sender, Windows.UI.Xaml.DragEventArgs e)
        {


        }

        
        private void Control_Drop(object sender, Windows.UI.Xaml.DragEventArgs e)
        {

            DataPackageView dpView = e.Data.GetView();
            var item = dpView.Properties[DragItemKey];

            var sourceItems = dpView.Properties[SourceItemsKey] as IList;

            var destinationItems = Element.ItemsSource as IList;

            var originalSrc = e.OriginalSource as FrameworkElement;

            var dataContext = originalSrc.DataContext;


            if (dataContext is ViewCell)
            {
                var x = destinationItems.IndexOf((dataContext as ViewCell).BindingContext);

                destinationItems.Insert(x, item);
            }
            else if (dataContext is CellItem)
            {
                var x = destinationItems.IndexOf((dataContext as CellItem));

                destinationItems.Insert(x, item);
            }
            else
                destinationItems.Add(item);

            sourceItems.Remove(item);
        }
    }
}

