// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System;
using System.Collections.Generic;
using System.Text;
namespace QuickGrammar
{
    public class RuleCompiler : IRuleCompiler
    {
        const char DEFAULT_BEGIN_SYMBOL_CHARACTER = '[';
        const char DEFAULT_END_SYMBOL_CHARACTER = ']';
        const char DEFAULT_BEGIN_OPTION_CHARACTER = '(';
        const char DEFAULT_END_OPTION_CHARACTER = ')';
        const char DEFAULT_DIVIDE_SELECTION_CHARACTER = '|';
        const char DEFAULT_BEGIN_MEMO_CHARACTER = '<';
        const char DEFAULT_END_MEMO_CHARACTER = '>';
        const char DEFAULT_DIVIDE_MEMO_CHARACTER = '$';
        const char DEFAULT_ESCAPE_CHARACTER = '\\';

        public static RuleCompiler Default
        {
            get
            {
                return new RuleCompiler(
                    DEFAULT_BEGIN_SYMBOL_CHARACTER,
                    DEFAULT_END_SYMBOL_CHARACTER,
                    DEFAULT_BEGIN_OPTION_CHARACTER,
                    DEFAULT_END_OPTION_CHARACTER,
                    DEFAULT_DIVIDE_SELECTION_CHARACTER,
                    DEFAULT_BEGIN_MEMO_CHARACTER,
                    DEFAULT_END_MEMO_CHARACTER,
                    DEFAULT_DIVIDE_MEMO_CHARACTER,
                    DEFAULT_ESCAPE_CHARACTER
                );
            }
        }

        readonly char BeginSymbolCharacter;
        readonly char EndSymbolCharacter;
        readonly char BeginOptionCharacter;
        readonly char EndOptionCharacter;
        readonly char DivideSelectionCharacter;
        readonly char BeginMemoCharacter;
        readonly char EndMemoCharacter;
        readonly char DivideMemoCharacter;
        readonly char EscapeCharacter;

        public RuleCompiler(
            char beginSymbolCharacter,
            char endSymbolCharacter,
            char beginOptionCharacter,
            char endOptionCharacter,
            char divideSelectionCharacter,
            char beginMemoCharacter,
            char endMemoCharacter,
            char divideMemoCharacter,
            char escapeCharacter
        )
        {
            BeginSymbolCharacter = beginSymbolCharacter;
            EndSymbolCharacter = endSymbolCharacter;
            BeginOptionCharacter = beginOptionCharacter;
            EndOptionCharacter = endOptionCharacter;
            DivideSelectionCharacter = divideSelectionCharacter;
            BeginMemoCharacter = beginMemoCharacter;
            EndMemoCharacter = endMemoCharacter;
            DivideMemoCharacter = divideMemoCharacter;
            EscapeCharacter = escapeCharacter;
        }

        public RuleCompiler() : this(
            DEFAULT_BEGIN_SYMBOL_CHARACTER,
            DEFAULT_END_SYMBOL_CHARACTER,
            DEFAULT_BEGIN_OPTION_CHARACTER,
            DEFAULT_END_OPTION_CHARACTER,
            DEFAULT_DIVIDE_SELECTION_CHARACTER,
            DEFAULT_BEGIN_MEMO_CHARACTER,
            DEFAULT_END_MEMO_CHARACTER,
            DEFAULT_DIVIDE_MEMO_CHARACTER,
            DEFAULT_ESCAPE_CHARACTER) {}

        readonly StringBuilder Builder = new StringBuilder();

        public Rule Compile(string ruletext)
        {
            return Compile(ruletext.GetEnumerator());
        }

        Rule Compile(CharEnumerator enumerator)
        {
            bool escape = false;
            bool symbol = false;
            Builder.Length = 0;
            List<Rule> rules = new List<Rule>();
            List<Rule> selections = null;

            while (enumerator.MoveNext())
            {
                char character = enumerator.Current;
                if (escape)
                {
                    if (character == 'n') Builder.Append('\n');
                    else Builder.Append(character);
                    escape = false;
                }
                else
                {
                    if (character == EscapeCharacter)
                    {
                        escape = true;
                    }
                    else if (character == BeginSymbolCharacter && !symbol)
                    {
                        Flush(rules);
                        symbol = true;
                    }
                    else if (character == EndSymbolCharacter && symbol)
                    {
                        FlushSymbol(rules);
                        symbol = false;
                    }
                    else if (character == BeginOptionCharacter || character == BeginMemoCharacter)
                    {
                        Flush(rules);
                        rules.Add(Compile(enumerator));
                    }
                    else if (character == DivideSelectionCharacter || character == DivideMemoCharacter)
                    {
                        Flush(rules);
                        if (selections == null)
                        {
                            selections = new List<Rule>();
                        }
                        selections.Add(BuildRule(rules));
                    }
                    else if (character == EndOptionCharacter)
                    {
                        Flush(rules);
                        if (selections == null)
                        {
                            if (rules.Count == 0)
                            {
                                return Rule.Empty;
                            }
                            else
                            {
                                return Rules.Optional(BuildRule(rules));
                            }
                        }
                        else
                        {
                            selections.Add(BuildRule(rules));
                            return Rules.Selection(selections);
                        }
                    }
                    else if (character == EndMemoCharacter)
                    {
                        if (Builder.Length > 0 && selections.Count > 0)
                        {
                            rules.Add(Rules.Memo(selections[0], Builder.ToString()));
                            Builder.Length = 0;
                            selections.Clear();
                        }
                    }
                    else
                    {
                        Builder.Append(character);
                    }
                }
            }
            Flush(rules);
            return BuildRule(rules);
        }

        void Flush(List<Rule> rules)
        {
            if (Builder.Length > 0)
            {
                rules.Add(Rules.Text(Builder.ToString()));
                Builder.Length = 0;
            }
        }

        void FlushSymbol(List<Rule> rules)
        {
            if (Builder.Length > 0)
            {
                rules.Add(Rules.Symbol(Builder.ToString()));
                Builder.Length = 0;
            }
        }

        Rule BuildRule(List<Rule> rules)
        {
            Rule rule;
            if (rules.Count == 0)
            {
                rule = Rule.Empty;
            }
            else if (rules.Count == 1)
            {
                rule = rules[0];
            }
            else
            {
                rule = Rules.Compound(rules);
            }
            rules.Clear();
            return rule;
        }
    }
}
