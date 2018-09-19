using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrcodeReader.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QrcodeReader
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormPage : ContentPage
    {
        public FormPage(FormData formData)
        {
            InitializeComponent();
        }

    }
}