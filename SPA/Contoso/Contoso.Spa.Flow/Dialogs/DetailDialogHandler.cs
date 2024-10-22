﻿using Contoso.Spa.Flow.Requests;
using System;

namespace Contoso.Spa.Flow.Dialogs
{
    public class DetailDialogHandler : BaseDialogHandler
    {
        public override void Complete(IFlowManager flowManager, RequestBase request)
        {
            if (request.CommandButtonRequest == null)
                throw new ArgumentException($"{nameof(request.CommandButtonRequest)}: {{59B88099-D16F-4C53-8E2D-0E352E40D210}}");

            if (request is not DetailRequest detailRequest)
                throw new ArgumentException($"{nameof(request)}: {{F63094B0-F9C6-4AA1-B896-002B372E5690}}");

            if (!request.CommandButtonRequest.Cancel
                && detailRequest.Entity != null)
            {
                flowManager.FlowDataCache.Items[detailRequest.Entity.GetType().FullName ?? throw new ArgumentException($"{nameof(request)}: {{07006628-25D9-46D2-8A6D-91BD3BD276E7}}")] = detailRequest.Entity;
            }

            base.Complete(flowManager, request);
        }
    }
}
