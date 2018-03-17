// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System.Text;
namespace QuickGrammar
{
    class MemoRule : Rule
    {
        readonly Rule Rule;
        readonly string Memo;

        internal MemoRule(Rule rule, string memo)
        {
            Rule = rule;
            Memo = memo;
        }

        public override void Evaluate(IRuleContext context, StringBuilder builder)
        {
            string result = Rule.Evaluate(context);
            context[Memo] = new TextRule(result);
            builder.Append(result);
        }
    }
}
