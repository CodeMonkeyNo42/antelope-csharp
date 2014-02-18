﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.PersisenceModule.Datamodule
{
    public interface IDatamodul
    {
        int Id { get; set; }
        object GetPostObject();
        object GetPutObject();
        string GetRequestUrlPart();
    }
}
