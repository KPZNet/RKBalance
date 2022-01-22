using RKBalance.Services;
using RKBalance.Models;
using RKBalance.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RKBalance
{
    public partial class App : Application
    {
        public SPBalanceRun spbData { get; set; }
        public App()
        {
            InitializeComponent();
            //Register Syncfusion license
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQ3OTY4QDMxMzgyZTMxMmUzMGh5ekJuS1lTb1FhWDJEUTlGRkZZNkJkTU1zenRRMUJmY0VHRmxYaUo4Rnc9;MjQ3OTY5QDMxMzgyZTMxMmUzMGk5aFZxVDJVZ3hUS3psa1g2NGpoOWp2WW5YWDJ2M3dWSzJCejN3QWpaZjg9;MjQ3OTcwQDMxMzgyZTMxMmUzMG90ODR0Z0NpVGZCbFpxUGRCczh0dTJWSTQ3aG9kQ1g3R1paMVhXZjIvV3M9;MjQ3OTcxQDMxMzgyZTMxMmUzMGpDRkxza1lWMFBYa3Z4VjhEQmVnWnFRYzZwai9ldVBwZ3RVbWhFUjc0c289;MjQ3OTcyQDMxMzgyZTMxMmUzMEpxQXVXOVNFU1JmMG9UZHo5UHZCdTdSM1h4Z2tyMGJCcnplL2JxZXYvdWc9;MjQ3OTczQDMxMzgyZTMxMmUzMFZ2QnI5S1F3ZktxVk4xYlJoTk5GZiswTktRNE4xbHB6ajJ3WGRlWk82a2M9;MjQ3OTc0QDMxMzgyZTMxMmUzMGhtRE5IQjJEVEI5bnNPdlVRQ1FhS1RqRE5LcTFvOTc1eS9QTWtGWHNvdnM9;MjQ3OTc1QDMxMzgyZTMxMmUzMGZsRmNndkt2aVQ2OTcyeGhjaThFWERReUNna29NVElad0o0RXczcFA5azQ9;MjQ3OTc2QDMxMzgyZTMxMmUzMEJnazg2TlZya0VxRjlxRjBVNS9RbXIxMWpRamQxUlltQlgyc3dncEYvK3c9;NT8mJyc2IWhia31ifWN9Z2FoYmF8YGJ8ampqanNiYmlmamlmanMDHmg4Nj0wNjQ/OjITND4yOj99MDw+;MjQ3OTc3QDMxMzgyZTMxMmUzMGJkYjM0R3VWZ1FXUndPdjlabmZiUTNodkZSWm1OZVd1U29qeTczaWQvdDA9");


            SinglePlaneBalanceVectorTools SPBalance = new SinglePlaneBalanceVectorTools();
            spbData = new SPBalanceRun();

            spbData.InitialRunVector.MagnitudeStr = "13.0";
            spbData.InitialRunVector.PhaseStr = "34";
            spbData.InitialRunVector.Color = SkiaSharp.SKColors.Blue;

            spbData.TrialWeightVector.MagnitudeStr = "50";
            spbData.TrialWeightVector.PhaseStr = "120";
            spbData.TrialWeightVector.Color = SkiaSharp.SKColors.Red;

            spbData.TrialRunVector.MagnitudeStr = "20.52";
            spbData.TrialRunVector.PhaseStr = "110";
            spbData.TrialRunVector.Color = SkiaSharp.SKColors.BlueViolet;

            spbData.WeightPlacementVector.Color = SkiaSharp.SKColors.SeaGreen;

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
