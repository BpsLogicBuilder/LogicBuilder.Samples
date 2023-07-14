using AutoMapper;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Domain;
using Contoso.Parameters.Expressions;
using Contoso.Spa.Flow.Properties;
using Contoso.Spa.Flow.Requests.TransientFlows;
using Contoso.Spa.Flow.Responses.TransientFlows;
using System;

namespace Contoso.Spa.Flow
{
    public class TransientFlowHelper : ITransientFlowHelper
    {
        private readonly IFlowManager _flowManager;
        private readonly IMapper _mapper;

        public TransientFlowHelper(IFlowManager flowManager, IMapper mapper)
        {
            _flowManager = flowManager;
            _mapper = mapper;
        }

        public BaseFlowResponse RunSelectorFlow(SelectorFlowRequest selectorFlowRequest)
        {
            EntityModelBase entity = selectorFlowRequest.Entity ?? throw new ArgumentException($"{nameof(selectorFlowRequest)}: {{9B9FC188-D1EC-4E1A-B71C-26A4E2413ABB}}");
            string flowName = selectorFlowRequest.ReloadItemsFlowName ?? throw new ArgumentException($"{nameof(selectorFlowRequest)}: {{56C7E26E-C4FD-46B9-99E3-BF2B544A8D84}}");
            this._flowManager.FlowDataCache.Items[entity.GetType().FullName ?? throw new ArgumentException($"{nameof(selectorFlowRequest)}: {{27C817BC-1EC1-442F-9483-30AD6B2BCD7E}}")] = entity;

            this._flowManager.RunFlow(flowName);

            if (!_flowManager.FlowDataCache.Items.TryGetValue(typeof(SelectorLambdaOperatorParameters).FullName!, out object? selector))
                throw new InvalidOperationException(Resources.selectorIsNotSet);

            return new SelectorFlowResponse
            {
                Success = true,
                Selector = _mapper.Map<SelectorLambdaOperatorDescriptor>
                (
                    selector
                )
            };
        }
    }
}
