// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System;
using System.Collections.Generic;
namespace QuickGrammar
{
    public class GrammarContext : IRuleContext
    {

        protected readonly IGrammar Grammar;
        protected readonly Random Random;
        protected readonly Dictionary<string, Rule> Memos = new Dictionary<string, Rule>();

        public GrammarContext(IGrammar grammar, Random random)
        {
            Grammar = grammar;
            Random = random;
        }

        public virtual Rule this[string symbol]
        {
            get
            {
                Rule rule;
                if (Memos.TryGetValue(symbol, out rule))
                {
                    return rule;
                }
                return Grammar[symbol];
            }
            set
            {
                Memos[symbol] = value;
            }
        }

        public virtual bool Optional()
        {
            return Random.Next(2) == 1;
        }

        public virtual int Select(int max)
        {
            return Random.Next(max);
        }


    }
}
