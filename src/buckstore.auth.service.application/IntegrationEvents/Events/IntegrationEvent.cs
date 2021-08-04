using System;
using MediatR;

namespace buckstore.auth.service.application.IntegrationEvents.Events
{
    public class IntegrationEvent: INotification
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
