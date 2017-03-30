using Xamarin.Forms;

namespace Mobile_RSS_Reader.UI.Styles
{   
    /// <summary>
    /// Represent feed card styling
    /// </summary>
    public static class FeedCardStyling
    {
        static FeedCardStyling()
        {
            CardSpacing = Device.OnPlatform(Android: 6, iOS: 8, WinPhone: 8);

            CardPadding = new Thickness(15, 10, 15, 10);

            CardDataStackLayoutStyle = new Style(typeof(StackLayout))
            {
                Setters =
                {
                    new Setter {Property = StackLayout.OrientationProperty, Value = StackOrientation.Vertical},
                    new Setter {Property = StackLayout.HorizontalOptionsProperty, Value = LayoutOptions.Fill},
                    new Setter {Property = StackLayout.VerticalOptionsProperty, Value = LayoutOptions.Fill},
                    new Setter {Property = StackLayout.MarginProperty, Value = new Thickness(0)},
                }
            };

            CardTitleStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.FromRgb(0x38, 0x38, 0x38)},
                    new Setter {Property = Label.FontSizeProperty, Value = 15},
                }
            };

            CardDetailTextStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.FromRgb(0x8D, 0x8D, 0x8D)},
                    new Setter {Property = Label.FontSizeProperty, Value = 12},
                }
            };

            CardDateTextStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.FromRgb(0xAA, 0xAA, 0xAA)},
                    new Setter {Property = Label.FontSizeProperty, Value = 12},
                }
            };
        }

        public static double CardSpacing { get; }

        public static Thickness CardPadding { get; }

        public static Style CardDataStackLayoutStyle { get; }

        public static Style CardTitleStyle { get; }

        public static Style CardDetailTextStyle { get; }

        public static Style CardDateTextStyle { get; }

        public static void Apply(ResourceDictionary resourceDictionary)
        {
            resourceDictionary.Add(nameof(CardSpacing), CardSpacing);
            resourceDictionary.Add(nameof(CardPadding), CardPadding);
            resourceDictionary.Add(nameof(CardDataStackLayoutStyle), CardDataStackLayoutStyle);
            resourceDictionary.Add(nameof(CardTitleStyle), CardTitleStyle);
            resourceDictionary.Add(nameof(CardDetailTextStyle), CardDetailTextStyle);
            resourceDictionary.Add(nameof(CardDateTextStyle), CardDateTextStyle);
        }
    }
}