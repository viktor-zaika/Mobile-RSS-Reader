using Xamarin.Forms;

namespace Mobile_RSS_Reader.UI.Controls
{
    /// <summary>
    /// Represent simple cardview control implementation.
    /// </summary>
    public class CardView : Frame
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CardView()
        {
            Padding = 0;
            if (Device.OS == TargetPlatform.iOS)
            {
                HasShadow = false;
                OutlineColor = Color.Transparent;
                BackgroundColor = Color.Transparent;
            }
        }
    }
}