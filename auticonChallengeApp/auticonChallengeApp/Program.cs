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



                // pseudocode for getting reconstruction pattern

                // foreach a-z letter
                // foreach word in original words
                // if the letter is in the original word
                // look at what comes after it in the corrupted word
                // if that character is not a-z, add it to the corruption pattern
                // continue, move on to next a-z letter to check, because we already found one in at the current word


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



                // try and recreate the corrupted word list from the original word list and our pattern
                List<string> reconstructedCorruptedWords = new List<string>();
                foreach (string word in originalWords)
                {
                    string reconstructedWord = word;

                    // foreach a-z letter
                    for (char letter = 'a'; letter <= 'z'; letter++)
                    {
                        // if the letter is in the corruption pattern
                        if (corruptionPattern.ContainsKey(letter))
                        {
                            char corruptedChar = corruptionPattern[letter];

                            // insert the corrupted character after the letter in the reconstructed word
                            int letterIndex = reconstructedWord.IndexOf(letter);
                            if (letterIndex != -1 && (letterIndex + 1) < reconstructedWord.Length)
                            {
                                // insert the corrupted character after the letter
                                reconstructedWord = reconstructedWord.Insert(letterIndex + 1, corruptedChar.ToString());
                            }
                            else if (letterIndex != -1) // if the letter is at the end of the word
                            {
                                reconstructedWord += corruptedChar;
                            }
                        }
                    }

                    reconstructedCorruptedWords.Add(reconstructedWord);
                }


                // wrap up
                // print out original word list (corrupted word list with symbols removed)
                Console.WriteLine("\nOriginal Word List:\n");
                foreach (string word in originalWords) {
                    Console.WriteLine(word);
                }
                Console.WriteLine("\n\n");

                // print out words that have been corrupted
                Console.WriteLine("\nWords that have been modified (" + modifiedWords.Count + ") modified:\n");
                foreach (string word in modifiedWords) {
                    Console.WriteLine(word);
                }

                // print out corruption pattern identified (wrong currently)
                Console.WriteLine("\nCorruption Pattern:\n");
                foreach (var kvp in corruptionPattern) {
                    Console.WriteLine($"{kvp.Key} -> {kvp.Key}{kvp.Value}");
                }

                // print out reconstructed corrupted word list (wrong currently)
                Console.WriteLine("\nReconstructed corrupted word list\n");
                foreach (string word in reconstructedCorruptedWords) {
                    Console.WriteLine(word);
                }

            } 

            catch (Exception ex) {
                Console.WriteLine($"an error occured during processing: {ex.Message}");
                return;
            }
        }
    }
}
