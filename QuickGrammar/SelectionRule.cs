// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System.Text;
namespace QuickGrammar
{
    class SelectionRule : Rule
    {
        readonly Rule[] Rules;

        internal SelectionRule(Rule[] rules)
        {
            Rules = rules;
        }

        public override void Evaluate(IRuleContext context, StringBuilder builder)
        {
            Rules[context.Select(Rules.Length)].Evaluate(context, builder);
        }
    }
}
