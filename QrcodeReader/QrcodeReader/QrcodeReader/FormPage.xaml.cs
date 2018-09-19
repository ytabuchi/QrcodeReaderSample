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
        private FormData _formData = new FormData();

        public FormPage(FormData formData)
        {
            InitializeComponent();

            if (formData != null)
                _formData = formData;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Category.Text = _formData.Category;
            Machine.Text = _formData.Machine;
            SerialNumber.Text = _formData.SerialNumber;

            var savedData = FileSystemClient.Instance.ReadFile();
            if (savedData != null)
            {
                LastName.Text = savedData.LastName;
                FirstName.Text = savedData.FirstName;
                Email.Text = savedData.Email;
                Telephone.Text = savedData.Telephone;
                Comment.Text = savedData.Comment;
            }
        }

        private async void SubmitClicked(object sender, EventArgs e)
        {
            var sendData = new FormData
            {
                Category = Category.Text,
                Machine = Machine.Text,
                SerialNumber = SerialNumber.Text,
                LastName = LastName.Text,
                FirstName = FirstName.Text,
                Email = Email.Text,
                Telephone = Telephone.Text,
                Comment = Comment.Text
            };

            //var result = await WebApiClient.Instance.PostFormDataAsync(sendData);
            var result = 1;

            var history = new SendHistory
            {
                DataId = sendData.Id,
                IsSent = result,
                SentDate = DateTimeOffset.Now
            };

            var client = new SQLiteClient();
            client.AddData(history);

            if (result != 0)
            {
                FileSystemClient.Instance.WriteFile(sendData);
                await DisplayAlert("Done", "Form data is sent.", "OK");
            }
        }

        private void ClearClicked(object sender, EventArgs e)
        {

        }
    }
}