// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System.IO;
namespace QuickGrammar
{
    public class GrammarReader
    {
        protected IRuleCompiler Compiler;

        public GrammarReader(IRuleCompiler compiler)
        {
            Compiler = compiler;
        }

        public void Read(string path, IGrammar grammar)
        {
            using (TextReader reader = File.OpenText(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int equalsIndex = line.IndexOf('=');
                    if (equalsIndex == -1) continue;

                    string name = line.Substring(0, equalsIndex).Trim();
                    string ruletext = line.Substring(equalsIndex + 1);

                    if (name.Length == 0) continue;

                    grammar[name] = Compiler.Compile(ruletext);

                }
            }
        }

    }
}
