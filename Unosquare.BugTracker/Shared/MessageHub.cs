namespace Unosquare.BugTracker.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// An overly-simplified message hub. For a full implementation see our
    /// awesome: https://github.com/unosquare/swan#the-messagehub
    /// </summary>
    public class MessageHub
    {
        private static readonly MessageHub m_Instance = new MessageHub();
        private readonly object SyncLock = new object();
        private readonly Dictionary<string, List<Func<object, Task>>> Subscribers = new Dictionary<string, List<Func<object, Task>>>();

        /// <summary>
        /// Prevents a default instance of the <see cref="MessageHub"/> class from being created.
        /// </summary>
        private MessageHub()
        {
            // placeholder
        }

        /// <summary>
        /// Gets the singleton instance of the message hub
        /// </summary>
        public static MessageHub Current
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="payload">The payload.</param>
        public void Publish(string name, object payload)
        {
            var listeners = new List<Action<object>>();
            lock (SyncLock)
            {
                var callbacks = Subscribers.ContainsKey(name) ? 
                    Subscribers[name] : new List<Func<object, Task>>();

                foreach (var c in callbacks)
                {
                    var t = c.Invoke(payload);
                }
            }
        }

        /// <summary>
        /// Subscribes to the message with the specified name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="callback">The callback.</param>
        public void Subscribe(string name, Func<object, Task> callback)
        {
            lock (SyncLock)
            {
                if (Subscribers.ContainsKey(name) == false)
                    Subscribers[name] = new List<Func<object, Task>>();

                Subscribers[name].Add(callback);
            }
        }

        /// <summary>
        /// Unsubscribes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="callback">The callback.</param>
        /// <exception cref="System.NotImplementedException">This is just a sample program. Keeping it at a minimum!</exception>
        public void Unsubscribe(string name, Action<object> callback)
        {
            throw new NotImplementedException("This is just a sample program. Keeping it at a minimum!");
        }
    }
}
