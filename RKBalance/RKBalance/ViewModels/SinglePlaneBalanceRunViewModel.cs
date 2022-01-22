using RKBalance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RKBalance.ViewModels
{
    public class SinglePlaneBalanceRunViewModel : INotifyPropertyChanged
    {
        public SinglePlaneBalanceRunViewModel()
        {
            InitialRun.Magnitude = 1.0;
            InitialRun.Phase = 45;

            TrialWeight.Magnitude = 1.0;
            TrialWeight.Phase = 45;

            InfluenceVector.Magnitude = 1.0;
            InfluenceVector.Phase = 45;

            WeightPlacement.Magnitude = 1.0;
            WeightPlacement.Phase = 45;
        }

        public STVector InitialRun { get; set; } = new STVector();
        public STVector TrialWeight { get; set; } = new STVector();
        public STVector InfluenceVector { get; set; } = new STVector();
        public STVector WeightPlacement { get; set; } = new STVector();

        private bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}