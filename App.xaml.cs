using MauiApp3.Page;

namespace MauiApp3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AdjustGlobalFontSize();

            // Подписка на изменение ориентации и разрешения экрана
            DeviceDisplay.MainDisplayInfoChanged += (s, e) => AdjustGlobalFontSize();
            MainPage = new NavigationPage(new MainPage()); ;   
        }
        private void AdjustGlobalFontSize()
        {
            double screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            double screenHight = DeviceDisplay.MainDisplayInfo.Height/ DeviceDisplay.MainDisplayInfo.Density;
            double dynamicFontSize = screenWidth * 0.04; // 4% от ширины экрана
            double dynamicFontSizeButton = screenWidth * 0.07; // 5% от ширины экрана
            double WidthButtonZoo = screenWidth * 0.95;
            double HightImageLogo = screenHight * 0.3;
            double WidthImageButtonQuest = screenWidth * 0.3;
            double HightImageButtonQuest = screenHight * 0.2;
            HightImageButtonQuest = Math.Clamp(HightImageButtonQuest, 0, 100);
            // Обновляем глобальный ресурс
            Current.Resources["GlobalFontSize"] = dynamicFontSize;
            Current.Resources["GlobalFontSizeButton"] = dynamicFontSizeButton;
            Current.Resources["GlobalWidthButtonZoo"] = WidthButtonZoo;
            Current.Resources["GlobalHightImageLogo"] = HightImageLogo;
            Current.Resources["GlobalWidhtImageButtonQuest"] = WidthImageButtonQuest;
            Current.Resources["GlobalHightImageButtonQuest"] = HightImageButtonQuest;
        }
    }
}
