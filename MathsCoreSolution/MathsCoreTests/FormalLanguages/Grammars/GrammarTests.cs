using System.Linq;
using CSharpExtensionsTests;
using MathsCore.FormalLanguages.Grammars;
using MathsCore.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.FormalLanguages.Grammars
{
    [TestClass]
    public class GrammarTests
    {
        public Grammar G
        {
            get
            {
                return new Grammar
                {
                    Nonterminals = new Set<string> { "S" },
                    Terminals = new Set<string> { "a", "b" },
                    Productions = new Set<Production>
                    {
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "aSb"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "ba"
                        }
                    },
                    Start = "S"
                };
            }
        }

        [TestMethod]
        public void Strings_containing_all_terminals_should_be_terminal()
        {
            G.IsTerminal("ab").ShouldBeTrue();
            G.IsTerminal("Sab").ShouldBeFalse();
        }

        [TestMethod]
        public void Nonterminal_indices_should_be_correct()
        {
            var indices = G.NonterminalIndices("Sab").ToList();
            indices.ShouldContainExactly(0);
            indices = G.NonterminalIndices("SabSS").ToList();
            indices.ShouldContainExactly(0, 3, 4);
        }
    }
}