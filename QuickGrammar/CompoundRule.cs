// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System.Text;
namespace QuickGrammar
{
    class CompoundRule : Rule
    {
        readonly Rule[] Rules;

        internal CompoundRule(Rule[] rules)
        {
            Rules = rules;
        }

        public override void Evaluate(IRuleContext context, StringBuilder builder)
        {
            foreach (Rule rule in Rules)
            {
                rule.Evaluate(context, builder);
            }
        }
    }
}
