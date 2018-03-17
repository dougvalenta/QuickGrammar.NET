// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System.Text;
namespace QuickGrammar
{
    class SymbolRule : Rule
    {
        readonly string Symbol;

        internal SymbolRule(string symbol)
        {
            Symbol = symbol;
        }

        public override void Evaluate(IRuleContext context, StringBuilder builder)
        {
            context[Symbol].Evaluate(context, builder);
        }
    }
}
