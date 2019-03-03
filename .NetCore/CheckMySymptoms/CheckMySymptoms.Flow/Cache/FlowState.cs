using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckMySymptoms.Flow.Cache
{
    public class FlowState
    {
        public FlowState()
        {
        }

        public FlowState(FlowStateForSerilization flowState)
        {
            PreviousDataStack = new Stack<PreviousData>(flowState.PreviousDataStack.Reverse<PreviousData>());
            PreviousRepopulateStack = new Stack<ViewFieldsListPair>(flowState.PreviousRepopulateStack.Reverse<ViewFieldsListPair>());
            ForwardRepopulateStack = new Stack<ViewFieldsListPair>(flowState.ForwardRepopulateStack.Reverse<ViewFieldsListPair>());
        }

        public FlowState(Stack<PreviousData> previousDataStack, Stack<ViewFieldsListPair> previousRepopulateStack, Stack<ViewFieldsListPair> forwardRepopulateStack)
        {
            PreviousDataStack = previousDataStack;
            PreviousRepopulateStack = previousRepopulateStack;
            ForwardRepopulateStack = forwardRepopulateStack;
        }

        public Stack<PreviousData> PreviousDataStack { get; set; }
        public Stack<ViewFieldsListPair> PreviousRepopulateStack { get; set; }
        public Stack<ViewFieldsListPair> ForwardRepopulateStack { get; set; }
    }

    /// <summary>
    /// Required for JSON.Net stack serialization.
    /// Without converting to a list, the deserialized stack
    /// returns in reverse order.
    /// </summary>
    public class FlowStateForSerilization
    {
        public FlowStateForSerilization()
        {
        }

        public FlowStateForSerilization(FlowState flowState)
        {
            PreviousDataStack = new List<PreviousData>(flowState.PreviousDataStack);
            PreviousRepopulateStack = new List<ViewFieldsListPair>(flowState.PreviousRepopulateStack);
            ForwardRepopulateStack = new List<ViewFieldsListPair>(flowState.ForwardRepopulateStack);
        }

        public List<PreviousData> PreviousDataStack { get; set; }
        public List<ViewFieldsListPair> PreviousRepopulateStack { get; set; }
        public List<ViewFieldsListPair> ForwardRepopulateStack { get; set; }
    }
}
