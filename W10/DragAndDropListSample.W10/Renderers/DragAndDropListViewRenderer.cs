using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragAndDropListSample.W10;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using System.Collections;
using Windows.UI.Xaml;
using DragAndDropListSample;
using Windows.UI.Xaml.Controls;
using Windows.UI.Input;
using System.ComponentModel;
using Windows.ApplicationModel.DataTransfer;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Xaml.Data;

[assembly: ExportRenderer(typeof(DraggableListView), typeof(DragAndDropListViewRenderer))]
namespace DragAndDropListSample.W10
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
                listView.CanDrag = true;
                

                listView.DragItemsStarting += ListView_DragItemsStarting;
                listView.DragOver += Control_DragOver;
                listView.DragLeave += Control_DragLeave;
                listView.DragEnter += Control_DragEnter;
                listView.Drop += Control_Drop;
                listView.DragItemsCompleted += ListView_DragItemsCompleted;
                listView.DropCompleted += ListView_DropCompleted;
               
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

        private void ListView_DragItemsCompleted(ListViewBase sender, DragItemsCompletedEventArgs args)
        {
           
        }

        private void ListView_DropCompleted(UIElement sender, DropCompletedEventArgs args)
        {
           
        }

       
       
        private void ListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            e.Data.Properties.Add(DragItemKey, e.Items.FirstOrDefault());
            e.Data.Properties.Add(SourceItemsKey, Element.ItemsSource);

            e.Data.RequestedOperation = DataPackageOperation.Move;
            
        }


        private void Control_DragOver(object sender, Windows.UI.Xaml.DragEventArgs e)
        {

        }

        private void Control_DragLeave(object sender, Windows.UI.Xaml.DragEventArgs e)
        {


        }

        private void Control_DragEnter(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
            e.AcceptedOperation =DataPackageOperation.Move;
        }

        private void Control_Drop(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
            var item = (e.Data.Properties[DragItemKey] as ViewCell).BindingContext;
            var cc = e.GetDeferral();
            e.AcceptedOperation = DataPackageOperation.Move;          

            var sourceItems = e.Data.Properties[SourceItemsKey] as IList;
            var destinationItems = Element.ItemsSource as IList;

            var originalSrc = e.OriginalSource as FrameworkElement;

            var dataContext = originalSrc.DataContext;

            var currentItem = (dataContext as CollectionViewSource).View.CurrentItem;

            if (dataContext is CollectionViewSource)
            {
                destinationItems.Add(item);
            }
            else if (dataContext is ViewCell)
            {
                var x = destinationItems.IndexOf((dataContext as ViewCell).BindingContext);

                destinationItems.Insert(x, item);
            }
            else if (dataContext is CellItem)
            {
                var x = destinationItems.IndexOf((dataContext as CellItem));

                destinationItems.Insert(x, item);
            }

            sourceItems.Remove(item);

            cc.Complete();

        }
    }
}

