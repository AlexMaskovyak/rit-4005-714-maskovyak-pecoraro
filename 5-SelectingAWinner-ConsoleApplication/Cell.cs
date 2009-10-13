using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace _5_SelectingAWinner_ConsoleApplication
{
    /// <summary> Professor Schreiner's producer/consumer pattern cell class. </summary>
    public class Cell<T> {
        /// <summary> value to be passed. </summary>
        private T content;

        /// <summary> state of <c>content</c>. </summary>
        private bool full = false;

        /// <summary> thread-safe access to <c>content</c>. </summary>
        public T Value {
            get {
                lock (this) {
                    while (!full) Monitor.Wait(this);
                    full = false;
                    Monitor.PulseAll(this);
                    return content;
                }
            }
            set {
                lock (this) {
                    // Ignore new values if already full
                    if (full)
                        return;

                    full = true;
                    Monitor.PulseAll(this);
                    content = value;
                }
            }
        }
    }
}
