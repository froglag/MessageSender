using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageSender.Handlers
{
    /// <summary>
    /// Delegate representing an event handler for exceptions.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Event arguments containing the exception.</param>
    public delegate void ExceptionEventHandler(object sender, ExceptionEventArgs e);

    /// <summary>
    /// Event arguments class for exceptions.
    /// </summary>
    public class ExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// The exception object.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the ExceptionEventArgs class.
        /// </summary>
        /// <param name="exception">The exception object.</param>
        public ExceptionEventArgs(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
            }

            Exception = exception;
        }
    }
}
