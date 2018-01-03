namespace Unosquare.BugTracker.Core
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// Dependency Injection Container.
    /// This class is a Signleton and an oversimplification. For a real Container, plese see:
    /// https://github.com/unosquare/swan#the-dependencycontainer
    /// </summary>
    public class DependencyContainer
    {
        private static DependencyContainer m_Instance = null;
        private static readonly object SyncLock = new object();
        private readonly ConcurrentDictionary<Type, object> Instances = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Prevents a default instance of the <see cref="DependencyContainer"/> class from being created.
        /// </summary>
        private DependencyContainer()
        {
            // placeholder
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static DependencyContainer Instance
        {
            get
            {
                lock (SyncLock)
                {
                    if (m_Instance == null)
                        m_Instance = new DependencyContainer();

                    return m_Instance;
                }

            }
        }

        /// <summary>
        /// Registers the specified instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Register<T>(T instance)
            where T : class
        {
            Instances[typeof(T)] = instance;
        }

        /// <summary>
        /// Resolves the instance for the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T Resolve<T>()
            where T : class
        {
            if (Instances.TryGetValue(typeof(T), out object output))
                return output as T;

            return null;
        }

    }
}
