using System.Text;

namespace Febucci.UI.Core.Parsing
{
    /// <summary>
    /// Handles text parsing and rich text tags recognition
    /// </summary>
    public static class TextParser
    {
        public static string ParseText(string text, params TagParserBase[] rules)
        {
            if(rules == null || rules.Length == 0)
            {
                UnityEngine.Debug.LogWarning("No rules were provided to parse the text. Skipping");
                return text;
            }

            //PS At the moment, only for avoiding fails on domain reload
            //and multiple tags on different text sets
            foreach (var rule in rules)
            {
                rule.Initialize();
            }
            
            /*
            P.S. Calculating tags etc. is done inside this single method (and not split for each rule etc.)
            so that the text is only parsed once, and not multiple times for each rule - improving performance
            */
            StringBuilder result = new StringBuilder();

            // create an array of character from text
            var characters = text.ToCharArray();
            int len = characters.Length;
            bool foundTag;
            string fullTag;
            bool allowParsing = true;

            //For every character in text
            for(int textIndex = 0, realTextIndex = 0; textIndex < len; textIndex++)
            {
                foundTag = false;

                //searches for noparse first
                if (characters[textIndex] == '<')
                {
                    int closeIndex = text.IndexOf('>', textIndex + 1);
                    if(closeIndex>0)
                    {
                        int tagLength = closeIndex - textIndex + 1;
                        void PasteTagToText()
                        {
                            foundTag = true;
                            result.Append(fullTag);
                            textIndex = closeIndex;
                        }
                        
                        fullTag = text.Substring(textIndex, tagLength);
                        switch (fullTag.ToLower())
                        {
                            case "<noparse>":
                                allowParsing = false;
                                PasteTagToText();
                                break;
                            case "</noparse>":
                                allowParsing = true;
                                PasteTagToText();
                                break;
                        }
                    }
                }

                if (allowParsing && !foundTag)
                {
                    foreach (var rule in rules) //tries rich tags
                    {
                        if (characters[textIndex] == rule.startSymbol)
                        {
                            for (int endIndex = textIndex + 1; endIndex < len && !foundTag; endIndex++)
                            {
                                //If there's an opening symbol, skips since it's a new tag
                                if (characters[endIndex] == rule.startSymbol)
                                    break;

                                if (characters[endIndex] == rule.endSymbol)
                                {
                                    // Gets the length of the tag
                                    int tagLength = endIndex - textIndex - 1;

                                    if (tagLength == 0) //Skips empty tag
                                        break;

                                    if (rule.TryProcessingTag(
                                        text.Substring(textIndex + 1, tagLength),
                                        tagLength,
                                        realTextIndex,
                                        textIndex))
                                    {
                                        //controls if tag should be pasted to text anyways
                                        //e.g. in TMPro
                                        if (rule.shouldPasteTag)
                                            result.Append(text.Substring(textIndex, tagLength + 2));

                                        foundTag = true;
                                        textIndex = endIndex; //Tag processed, skips others
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (!foundTag)
                {
                    result.Append(characters[textIndex]);
                    realTextIndex++;
                }
            }
            
            return result.ToString();
        }
    }
}