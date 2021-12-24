using AutoMapper;
using Contoso.Forms.Parameters.Expansions;
using Contoso.Forms.View.Expansions;
using LogicBuilder.Expressions.Utils.Expansions;

namespace Contoso.Web.Utils
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
