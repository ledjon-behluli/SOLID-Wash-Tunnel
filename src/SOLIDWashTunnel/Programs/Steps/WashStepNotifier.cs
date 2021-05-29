using SOLIDWashTunnel.ClientFacing;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs.Steps
{
    /* 
   * Pattern: 
   *   Observer
   *
   * Reason: 
   *   Defines a subscription mechanism to notify multiple subscribers about any events that happen to the object they are observing.
   *   On each wash step a notification is sent to all subscribers. The subscribers are outside of this project (See: 'Sms' & 'MobileApp' projects).
   *   They can be used by the client, and provide extension methods which subscribe the particular subscriber they offer, to the 'WashStepNotifier'.
   *   The current project does not have any reference to them.
   *   
   * Learn more: 
   *   https://en.wikipedia.org/wiki/Observer_pattern
   */

    public interface IWashStepNotifier
    {
        void Subscribe(IWashStepSubscriber subscriber);
        void Unsubscribe(IWashStepSubscriber subscriber);
        void Notify(IWashStep state);
    }

    public class WashStepNotifier : IWashStepNotifier
    {
        private readonly List<IWashStepSubscriber> _subscribers;

        public WashStepNotifier()
        {
            _subscribers = new List<IWashStepSubscriber>();
        }

        public void Subscribe(IWashStepSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(IWashStepSubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void Notify(IWashStep state)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.OnStateChange(state);
            }
        }
    }
}