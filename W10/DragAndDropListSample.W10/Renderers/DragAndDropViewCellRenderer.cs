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


#if WINDOWS_APP || WINDOWS_PHONE_APP
using Xamarin.Forms.Platform.WinRT;
#else
using Xamarin.Forms.Platform.UWP;
#endif

[assembly:ExportRenderer(typeof(DraggableViewCell), typeof(DragAndDropViewCellRenderer))]
namespace DragAndDropListSample.W10
{
	public class DragAndDropViewCellRenderer : ViewCellRenderer
	{
       
        public override Windows.UI.Xaml.DataTemplate GetTemplate(Cell cell)
        {
            var _cell= base.GetTemplate(cell);           

            return _cell;
        }
    }
}

