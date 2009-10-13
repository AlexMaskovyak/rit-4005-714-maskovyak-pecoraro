using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using _5_SelectingAWinner_ConsoleApplication;

namespace _5_SelectingAWinner_WPFApplication {

    /// <summary>Cache for Images.</summary>
    public class PlayingCardCache {

        /// <summary> image URI prefix. </summary>
        protected string _prefix;

        /// <summary> image URI suffix. </summary>
        protected string _suffix;

        /// <summary> the images </summary>
        protected Dictionary<string, BitmapImage> _images;

        /// <summary> to handle multithreaded access </summary>
        private static object monitor = new object();

        /// <summary> private constructor to force singleton behavior. </summary>
        /// <param name="prefix"> the image uri prefix. </param>
        /// <param name="suffix"> the image uri suffix. </param>
        public PlayingCardCache(string prefix, string suffix) {
            _prefix = prefix;
            _suffix = suffix;
            _images = new Dictionary<string, BitmapImage>();
        }

        /// <summary> cache a provided image (suffix included). </summary>
        /// <param name="imageName"> the image name. </param>
        /// <returns> the image. </returns>
        public virtual BitmapImage Cache(string imageName) {
            lock (this) {
                if (_images.ContainsKey(imageName))
                    return _images[imageName];

                BitmapImage img = new BitmapImage(new Uri(_prefix + imageName));
                _images[imageName] = img;
                return img;
            }
        }

        /// <summary> cache the image for a card given a suit and rank. </summary>
        /// <param name="suit"> the card's suit. </param>
        /// <param name="rank"> the card's rank. </param>
        /// <returns> the image </returns>
        public virtual BitmapImage ImageForCard(int suit, int rank) {
            
            // TODO: Fix this to get the proper number...
            string cardIdentifier = ((4 * rank) + suit).ToString();
            lock (this) {
                if (_images.ContainsKey(cardIdentifier))
                    return _images[cardIdentifier];

                string uri = _prefix + cardIdentifier + _suffix;
                BitmapImage img = new BitmapImage(new Uri(uri));
                _images[cardIdentifier] = img;
                return img;
            }
        }

    }
}
