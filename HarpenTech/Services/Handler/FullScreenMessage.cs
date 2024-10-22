using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Services.Handler
{
    public class FullScreenMessage : ValueChangedMessage<object>
    {
        public FullScreenMessage(object r) : base(r)
        {
        }
    }

    public class NormalScreenMessage : ValueChangedMessage<object>
    {
        public NormalScreenMessage(object r) : base(r)
        {
        }
    }
}
