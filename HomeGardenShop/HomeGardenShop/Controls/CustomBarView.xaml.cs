using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HomeGardenShop.CustomViews;
using Prism.Events;
using Sharpnado.Tabs;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static HomeGardenShop.Controls.CustomBottomTabItem;

namespace HomeGardenShop.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomBarView : ContentView, IDisposable
    {
        public delegate void TapDelegate();
        private List<ViewItem> _items;
        private int number;
        public CustomBarView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IList),
            typeof(CustomBarView),
            propertyChanged: OnItemsSourceModified);

        public IList ItemsSource
        {
            get
            {
                return (IList)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly BindableProperty ItemSelectedProperty =
            BindableProperty.Create(nameof(ItemSelected), typeof(ICommand), typeof(CustomBarView), null);

        public ICommand ItemSelected
        {
            get
            {
                return (ICommand)GetValue(ItemSelectedProperty);
            }
            set
            {
                SetValue(ItemSelectedProperty, value);
            }
        }

        public static readonly BindableProperty BarBackgroundColorProperty =
            BindableProperty.Create(nameof(BarBackgroundColor), typeof(Color), typeof(CustomBarView), default(Color));

        public Color BarBackgroundColor
        {
            get
            {
                return (Color)GetValue(BarBackgroundColorProperty);
            }
            set
            {
                SetValue(BarBackgroundColorProperty, value);
            }
        }

        public static readonly BindableProperty BarDetailBackgroundColorProperty =
            BindableProperty.Create(nameof(BarDetailBackgroundColor), typeof(Color), typeof(CustomBarView), default(Color));

        public Color BarDetailBackgroundColor
        {
            get
            {
                return (Color)GetValue(BarDetailBackgroundColorProperty);
            }
            set
            {
                SetValue(BarDetailBackgroundColorProperty, value);
            }
        }

        private static void OnItemsSourceModified(object sender, object oldValue, object newValue)
        {
            if (!(sender is CustomBarView initialsView))
                return;

            var source = (IList)newValue;
            if (source != null)
            {
                var list = source.Cast<ViewItem>().ToList();
                initialsView.SetValue(list);
            }

        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            GetViewForScreen();
        }
        private void GetViewForScreen()
        {
            //if (DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape)
            //{
            //    stack.HorizontalOptions = LayoutOptions.CenterAndExpand;

            //}
            //else
            //{
            //    stack.HorizontalOptions = LayoutOptions.StartAndExpand;
            //}
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            //if (propertyName.Contains(nameof(BarDetailBackgroundColor)))
            //{
            //    DetailToolbar.BackgroundColor = BarDetailBackgroundColor;
            //}
        }
        public event EventHandler Clicked;
        private void SetValue(List<ViewItem> items)
        {
            //items.Insert(0, new ViewItem { ImageSource = "outline_arrow.png", Title = string.Empty });
            //DetailToolbar.BackgroundColor = BarDetailBackgroundColor;
            //var toolbar = SToolbar;
            //var index = 0;
            // var toolbarItems = new TabHostView.Tabs;

            //toolbar.ColumnDefinitions = new ColumnDefinitionCollection();
            _items = items;
            foreach (var item in items)
            {


                //TapGestureRecognizer _buttonGestureRecognizer = new TapGestureRecognizer();
                //_buttonGestureRecognizer.Command = new Command(() => ItemSelected.Execute(item));
                BottomTabItem tabItem = new BottomTabItem { Style = this.Resources["BottomTabStyle"] as Style, IconImageSource = item.ImageSource };
                //tabItem.GestureRecognizers.Add(_buttonGestureRecognizer);



                //toolbar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(items.Count + 1, GridUnitType.Star) });

                //var image = new ExtendedImageButton
                //{
                //    Source = item.ImageSource,
                //    HeightRequest = 27,
                //    WidthRequest = 27,
                //    //TintColor = Color.White,
                //    BackgroundColor = Color.Transparent,
                //    VerticalOptions = LayoutOptions.CenterAndExpand,
                //    HorizontalOptions = LayoutOptions.CenterAndExpand
                //};

                //if (index == 0)
                //{
                //    image.Clicked += async (s, e) =>
                //    {
                //        if (!DetailToolbar.IsVisible)
                //        {
                //            await image.RotateTo(180, 150);
                //            await this.TranslateTo(0.0f, -15f, 150);
                //            DetailToolbar.IsVisible = true;
                //        }
                //        else
                //        {
                //            await image.RotateTo(0, 150);
                //            await this.TranslateTo(0.0f, (items.Count - 1) * 44, 150);
                //            DetailToolbar.IsVisible = false;
                //        }
                //    };
                //}
                //else
                //{
                //}
                SToolbar.Tabs.Add(tabItem);
            }

            // SToolbar.Children.Add(toolbar, 0, 0);

            //SToolbar.HeightRequest = (items.Count - 1) * 44 + 50;
            //TranslationY = (items.Count - 1) * 44;
        }

        public void Dispose()
        {
            this?.Dispose();
        }


        void SToolbar_SelectedTabIndexChanged(System.Object sender, Xamarin.Forms.SelectedPositionChangedEventArgs e)
        {
            int position = (int)e.SelectedPosition;
            if (number != position)
            {
                ItemSelected.Execute(_items[position]);
                number = position;
            }
        }
    }

    public class ViewItem
    {
        public string Title { get; set; }
        public string ImageSource { get; set; }
    }
}

