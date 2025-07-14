namespace auticonChallengeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> corruptedWords;
            List<string> originalWords;

            try {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var corruptedPath = Path.Combine(baseDir, "wordlist_corrupted.txt");
                var originalPath = Path.Combine(baseDir, "wordlist_original.txt");

                // get the corrupted and original word lists
                corruptedWords = File.ReadAllLines(corruptedPath).ToList();
                originalWords = File.ReadAllLines(originalPath).ToList();

                // find words that have been modified
                List<string> modifiedWords = new List<string>();
                for (int i = 0; i < corruptedWords.Count; i++) if (!String.Equals(originalWords[i], corruptedWords[i])) modifiedWords.Add(originalWords[i]);

            

                
                // foreach a-z letter
                    // foreach word in original words
                        // if the letter is in the original word
                            // look at what comes after it in the corrupted word
                            // if that character is not a-z, add it to the corruption pattern
                            // continue, move on to next a-z letter to check, because we already found one in at the current word
                
                
              
            } 

            catch (Exception ex) {
                Console.WriteLine($"error reading word lists: {ex.Message}");
                return;
            }


            // look for patterns. see what (if any) corrupted symbol character after every a-z letter
            Dictionary<char, char> corruptionPattern = new Dictionary<char, char>();

            // foreach a-z letter
            for (char letter = 'a'; letter <= 'z'; letter++)
            {

                // foreach word in original words
                foreach (string word in originalWords)
                {

                    // if the letter is in the original word
                    if (word.Contains(letter))
                    {

                        // find the letterIndex of the letter in the original word
                        int letterIndex = word.IndexOf(letter);

                        // look at the corresponding character in the corrupted word
                        string corruptedWord = corruptedWords[originalWords.IndexOf(word)];
                        if ((letterIndex + 1) < corruptedWord.Length)
                        {
                            char nextChar = corruptedWord[letterIndex + 1];

                            // if that character is not a-z, add it to the corruption pattern
                            if (!char.IsLetter(nextChar))
                            {

                                if (!corruptionPattern.ContainsKey(letter))
                                {
                                    corruptionPattern[letter] = nextChar;
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine(corruptionPattern);
        }
    }
}
