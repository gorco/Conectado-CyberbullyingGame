using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xasu.HighLevel;

public static class PendingExtensions
{

    public static StatementPromise AddPendingExtensions(this StatementPromise promise, Dictionary<string, object> pendingExtensions)
    {
        promise.WithResultExtensions(pendingExtensions);
        pendingExtensions.Clear();
        return promise;
    }
}
