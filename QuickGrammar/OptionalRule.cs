// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System.Text;
namespace QuickGrammar
{
    class OptionalRule : Rule
    {
        readonly Rule Rule;

        internal OptionalRule(Rule rule)
        {
            Rule = rule;
        }

        public override void Evaluate(IRuleContext context, StringBuilder builder)
        {
            if (context.Optional())
            {
                Rule.Evaluate(context, builder);
            }
        }
    }
}
