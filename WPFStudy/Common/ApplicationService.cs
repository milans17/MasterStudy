using Prism.Events;

namespace WPFStudy.Common
{
    
    internal sealed class ApplicationService
    {
        private ApplicationService() { }

        private static readonly ApplicationService instance = new ApplicationService();

        internal static ApplicationService Instance { get { return instance; } }

        private IEventAggregator _eventAggregator;
        internal IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                {
                    lock (instance)
                    {
                        if (_eventAggregator == null)
                        {
                            return _eventAggregator = new EventAggregator();
                        }
                    }
                }

                return _eventAggregator;
            }
        }
    }
}
