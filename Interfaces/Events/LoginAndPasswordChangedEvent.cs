﻿using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Events
{
    public class LoginAndPasswordChangedEvent : CompositePresentationEvent<Tuple<string, string>>
    {
    }
}
