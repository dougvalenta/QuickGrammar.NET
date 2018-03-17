// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System.Collections.Generic;
namespace QuickGrammar
{
    public static class Rules
    {
        public static Rule Empty()
        {
            return Rule.Empty;
        }

        public static Rule Text(string text)
        {
            return new TextRule(text);
        }

        public static Rule Symbol(string symbol)
        {
            return new SymbolRule(symbol);
        }

        public static Rule Optional(Rule rule)
        {
            return new OptionalRule(rule);
        }

        public static Rule Selection(params Rule[] rules)
        {
            return new SelectionRule(rules);
        }

        public static Rule Selection(ICollection<Rule> rules)
        {
            Rule[] rulesArray = new Rule[rules.Count];
            rules.CopyTo(rulesArray, 0);
            return new SelectionRule(rulesArray);
        }

        public static Rule Compound(params Rule[] rules)
        {
            return new CompoundRule(rules);
        }

        public static Rule Compound(ICollection<Rule> rules)
        {
            Rule[] rulesArray = new Rule[rules.Count];
            rules.CopyTo(rulesArray, 0);
            return new CompoundRule(rulesArray);
        }

        public static Rule Memo(Rule rule, string memo)
        {
            return new MemoRule(rule, memo);
        }

    }
}
