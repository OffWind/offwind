using Irony.Parsing;

namespace Offwind.Products.OpenFoam.Parsing
{
    [Language("OpenFOAM", "2.1.x", "OpenFOAM property files grammar")]
    public class OpenFoamGrammar : global::Irony.Parsing.Grammar
    {
        public OpenFoamGrammar()
        {
            this.GrammarComments = "";

            ////Symbols
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
            var ofFvtUniform = ToTerm("uniform", "ofFvtUniform");
            var ofFvtNonUniform = ToTerm("nonuniform", "ofFvtNonUniform");

            //Nonterminals
            var ofValue = new NonTerminal("ofValue");
            var ofArray = new NonTerminal("ofArray");
            var ofArrayEntry = new NonTerminal("ofArrayEntry");
            var ofArrayWrapper = new NonTerminal("ofArrayWrapper");

            var ofDictionary = new NonTerminal("Dictionary");
            var ofDictionaryContent = new NonTerminal("DictionaryContent");
            var ofDictionaryContentWrapper = new NonTerminal("DictionaryContentWrapper");

            var dictEntry = new NonTerminal("dictEntry");
            var dictEntry_Basic = new NonTerminal("dictEntry_Basic");
            var dictEntry_DimensionSet = new NonTerminal("dictEntry_DimensionSet");
            var dictEntry_DimensionalScalar = new NonTerminal("dictEntry_DimensionalScalar");
            var dictEntry_Array = new NonTerminal("dictEntry_Array");
            var dictEntry_FieldValue = new NonTerminal("dictEntry_FieldValue");
            //var dictEntry_FieldValueArray = new NonTerminal("dictEntry_FieldValueArray");

            //Rules
            ofValue.Rule = ofIdentifier | ofString | ofNumber | "true" | "false" | "null";

            dictEntry_Basic.Rule = ofIdentifier + whitespace + ofValue + semicolon;
            dictEntry_DimensionSet.Rule = ofIdentifier + whitespace + "[" + ofNumber + ofNumber + ofNumber + ofNumber + ofNumber + ofNumber + ofNumber + "]" + semicolon;
            dictEntry_DimensionalScalar.Rule = ofIdentifier + whitespace + ofIdentifier + "[" + ofNumber + ofNumber + ofNumber + ofNumber + ofNumber + ofNumber + ofNumber + "]" + ofNumber + semicolon;
            dictEntry_Array.Rule = ofIdentifier + whitespace + ofArrayWrapper + semicolon;
            dictEntry_FieldValue.Rule = ofIdentifier + (ofFvtUniform | ofFvtNonUniform) + (ofNumber | ofArray) + semicolon;
            dictEntry.Rule = dictEntry_Basic
                | dictEntry_DimensionSet
                | dictEntry_DimensionalScalar
                | dictEntry_Array
                | ofDictionary
                | dictEntry_FieldValue
                ;

            ofDictionaryContent.Rule = MakeStarRule(ofDictionaryContent, whitespace, dictEntry);
            ofDictionaryContentWrapper.Rule = "{" + ofDictionaryContent + "}";
            ofDictionary.Rule = ofIdentifier + whitespace + ofDictionaryContentWrapper;

            ofArrayEntry.Rule = ofValue | ofArrayWrapper | ofDictionary;
            ofArray.Rule = MakeStarRule(ofArray, whitespace, ofArrayEntry);
            ofArrayWrapper.Rule = "(" + ofArray + ")";

            //Set grammar root
            this.Root = ofDictionaryContent;
            MarkPunctuation("{", "}", "[", "]", ":", ",");
            MarkTransient(ofValue, ofDictionaryContentWrapper);
            //MarkReservedWords("FoamFile");
        }
    }
}