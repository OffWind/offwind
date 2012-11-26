using Irony.Parsing;

namespace Offwind.Products.OpenFoam.Parsing
{
    [Language("OpenFOAM", "2.1.x", "OpenFOAM fvSchemes dictionary grammar")]
    public class NumericalSchemeGrammar : global::Irony.Parsing.Grammar
    {
        public NumericalSchemeGrammar()
        {
            this.GrammarComments = "";

            //Symbols
            var whitespace = new NonTerminal("WhiteSpace", Empty | " " | "\t");
            // Comments
            var singleLineComment = new CommentTerminal("SingleLineComment", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
            var multiLineComment = new CommentTerminal("DelimitedComment", "/*", "*/");
            NonGrammarTerminals.Add(singleLineComment);
            NonGrammarTerminals.Add(multiLineComment);

            //Terminals
            var semicolon = ToTerm(";", "Semicolon");
            var ofString = new StringLiteral("String", "\"");
            var ofNumber = new NumberLiteral("Number", NumberOptions.AllowSign);
            var ofIdentifier = TerminalFactory.CreateCSharpIdentifier("Identifier");
            var ofArrayTerm = new IdentifierTerminal("SchemeTerm", "(),|*.+-/%^~!&");

            //Non-terminals
            var ofValue = new NonTerminal("Value");

            var ofDictionary = new NonTerminal("Dictionary");
            var ofDictionaryContent = new NonTerminal("DictionaryContent");
            var ofDictionaryContentWrapper = new NonTerminal("DictionaryContentWrapper");
            var ofDictEntry = new NonTerminal("DictEntry");

            var ofArrayEntry = new NonTerminal("ArrayEntry");
            var ofArray = new NonTerminal("Array");
            var ofArrayWrapper = new NonTerminal("ArrayWrapper");

            //BNF
            ofValue.Rule = ofIdentifier | ofArrayTerm | ofString | ofNumber;

            ofDictEntry.Rule = ofArrayWrapper | ofDictionary;
            ofDictionaryContent.Rule = MakeStarRule(ofDictionaryContent, whitespace, ofDictEntry);
            ofDictionaryContentWrapper.Rule = "{" + ofDictionaryContent + "}";
            ofDictionary.Rule = ofIdentifier + whitespace + ofDictionaryContentWrapper;

            ofArrayEntry.Rule = ofValue;
            ofArray.Rule = MakeStarRule(ofArray, whitespace, ofArrayEntry);
            ofArrayWrapper.Rule = ofArray + semicolon;

            //Set grammar root
            this.Root = ofDictionaryContent;
            MarkPunctuation("{", "}");
            MarkTransient(ofValue, ofDictionaryContentWrapper);
        }
    }
}
