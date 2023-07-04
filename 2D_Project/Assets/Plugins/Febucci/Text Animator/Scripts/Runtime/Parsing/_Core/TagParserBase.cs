namespace Febucci.UI.Core.Parsing
{
    /// <summary>
    /// Base class to parse/process a rich text tag
    /// </summary>
    public abstract class TagParserBase
    {
        //--- SYMBOLS ---
        public char startSymbol;
        public char endSymbol;
        public char closingSymbol; //TODO remove closing symbol to all, add it only to regions

        public virtual bool shouldPasteTag => false;
        public TagParserBase() { }
        public TagParserBase(char startSymbol, char closingSymbol, char endSymbol)
        {
            this.startSymbol = startSymbol;
            this.closingSymbol = closingSymbol;
            this.endSymbol = endSymbol;
        }

        //--- METHODS ---
        public abstract bool TryProcessingTag(string textInsideBrackets, int tagLength, int realTextIndex, int internalOrder);

        public void Initialize() => OnInitialize();
        protected virtual void OnInitialize(){ }
    }
}