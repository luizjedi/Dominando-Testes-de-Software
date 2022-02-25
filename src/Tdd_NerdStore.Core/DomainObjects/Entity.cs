using System;
using System.Collections.Generic;
using Tdd_NerdStore.Core.Messages;

namespace Tdd_NerdStore.Core.DomainObjects
{
    public abstract class Entity
    {
        #region "Properties"
        public Guid Id { get; set; }

        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();
        #endregion

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        #region "Methods"
        public void AddEvent(Event _event)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(_event);
        }
        public void RemoveEvents(Event _eventItem)
        {
            _notifications?.Remove(_eventItem);
        }
        public void ClearEvents()
        {
            _notifications?.Clear();
        }
        #endregion
    }
}
