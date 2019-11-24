﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FplPriceNotificator.Scheduler
    {
   public interface IScheduledTask
   {
       string Schedule { get; }

       Task ExecuteAsync(CancellationToken cancellationToken);
   }
    }
