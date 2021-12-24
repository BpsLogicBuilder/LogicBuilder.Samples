using AutoMapper;
using Enrollment.Forms.Parameters.Expansions;
using Enrollment.Forms.View.Expansions;
using LogicBuilder.Expressions.Utils.Expansions;

namespace Enrollment.Web.Utils
{
    public static class MappingOperations
    {
        public static SelectExpandDefinition MapExpansion(this IMapper mapper, SelectExpandDefinitionParameters expression)
            => mapper.MapExpansion
            (
                mapper.Map<SelectExpandDefinitionView>(expression)
            );

        public static SelectExpandDefinition MapExpansion(this IMapper mapper, SelectExpandDefinitionView expression)
            => mapper.Map<SelectExpandDefinition>
            (
                expression
            );
    }
}
