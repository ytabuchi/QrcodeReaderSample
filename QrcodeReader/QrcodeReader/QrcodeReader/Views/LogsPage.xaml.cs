using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QrcodeReader.Models;
using Xamarin.Forms;

namespace QrcodeReader.Views
{
    public partial class LogsPage : ContentPage
    {
        private ObservableCollection<SendHistory> _history = new ObservableCollection<SendHistory>();

        public LogsPage()
        {
            InitializeComponent();

            this.BindingContext = _history;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // SQLiteからデータを取得して
            var client = new SQLiteClient();
            var logs = client.ReadAllData();

            _history.Clear();

            if (logs.Count > 0)
            {
                foreach (var log in logs)
                {
                    _history.Add(log);
                }
            }
        }
    }
}
