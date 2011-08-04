using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solyutor.EventPublisher
{
    public interface IAssignee
    {
        void Subscribe(object subcriber);

        void Unsubscribe(object subcriber);
    }
}
