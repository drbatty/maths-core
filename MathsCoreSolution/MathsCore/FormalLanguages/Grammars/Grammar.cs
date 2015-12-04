using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.Text;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Sets;

namespace MathsCore.FormalLanguages.Grammars
{
    public class Grammar
    {
        public Set<string> Nonterminals { get; set; }
        public Set<string> Terminals { get; set; }
        public Set<Production> Productions { get; set; }
        public string Start { get; set; }

        public Set<string> Symbols
        {
            get { return Terminals | Nonterminals; }
        }

        public bool IsTerminal(string s)
        {
            return Productions.None(p => s.Contains(p.Lhs));
        }

        public IEnumerable<int> NonterminalIndices(string s)
        {
            return 0.Upto(s.Length - 1).Where(i => s.Substring(i, 1) < Nonterminals);
        }

        public IEnumerable<int> ProducibleIndices(string s)
        {
            return 0.Upto(s.Length - 1).Where(i => s.Substring(i, 1) < Symbols);
        }

        public Set<Production> ProductionsContaining(string s)
        {
            return Productions.Where(p => p.Lhs == s).ToSet();
        }

        public string ApplyRandomProduction(string s)
        {
            if (IsTerminal(s))
                return s;
            var index = NonterminalIndices(s).TakeRandom();
            var productions = ProductionsContaining(s.Substring(index, 1)).ToList();
            if (productions.None())
                return s;
            var production = productions.TakeRandom();
            return s.ReplaceCharacter(index, production.Rhs);
        }

        public override string ToString()
        {
            return Start + ";" + Productions.Select(p => p.ToString()).CommaSeparate();
        }

        #region Examples

        public static Grammar TwoLetterEvenPalindromic
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
                            Rhs = "aSa"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "bSb"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = ""
                        }
                    },
                    Start = "S"
                };
            }
        }

        public static Grammar F2WordProblem
        {
            get
            {
                return new Grammar
                {
                    Nonterminals = new Set<string> { "S" },
                    Terminals = new Set<string> { "a", "b", "A", "B" },
                    Productions = new Set<Production>
                    {
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "aSA"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "bSB"
                        },
                         new Production
                        {
                            Lhs = "S",
                            Rhs = "ASa"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "BSb"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = ""
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "SAa"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "SBb"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "SaA",
                        },
                        new Production{
                            Lhs = "S",
                            Rhs = "SbB"
                        }
                    },
                    Start = "S"
                };
            }
        }

        public static Grammar OctagonGroupWordProblem
        {
            get
            {
                return new Grammar
                {
                    Nonterminals = new Set<string> { "S" },
                    Terminals = new Set<string> { "a", "b", "A", "B" },
                    Productions = new Set<Production>
                    {
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "aSA"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "bSB"
                        },
                         new Production
                        {
                            Lhs = "S",
                            Rhs = "ASa"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "BSb"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = ""
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "SAa"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "SBb"
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "SaA",
                        },
                        new Production
                        {
                            Lhs = "S",
                            Rhs = "SbB"
                        },
                        new Production
                        {
                            Lhs="a",
                            Rhs="dcDCbaB"
                        }
                        //add 31 others? generate programatically?
                    },
                    Start = "S"
                };
            }
        }

        #endregion
    }
}