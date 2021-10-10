using System.Linq;

namespace NetBot.Services.PigLatin
{
    public static class PigLatinService
    {
        // https://reference.yourdictionary.com/resources/how-to-speak-pig-latin-basic-rules.html
        // Word that begin with consonant : move the first letter at end and ad ay (dog -> ogday)
        // Word that begin with consonant cluster (more than 1 consonant) : move the cluster at end and add ay (plant -> antplay)
        // Begin with vowel : Add yay to the end (end -> endyay)
        // y as first letter : consonant
        // y as second letter : vowel
        // compound words : treated as two words

        private const char Y = 'Y';
        private const char y = 'y';
        private static readonly char[] Vowels = {'a', 'e', 'i', 'o', 'u' , 'A', 'E', 'I', 'O', 'U'};

        public static string Pigify(this string self)
        {
            return self.StartsWithVowel()
                ? $"{self}yay"
                : $"{self.RemoveStartConsonants()}{self.GetStartConsonants()}ay";
        }

        private static bool StartsWithVowel(this string self)
        {
            return Vowels.Contains(self[0]);
        }

        public static string GetStartConsonants(this string self)
        {
            var startConsonants =  self
                .TakeWhile(c => !Vowels.Contains(c) && c != Y && c != y || c is Y or y && self[0] == c)
                .ToArray();
            return new string(startConsonants);
        }

        public static string RemoveStartConsonants(this string self)
        {
            var chars = self
                .SkipWhile(aChar => !Vowels.Contains(aChar) && aChar is not Y and not y)
                .ToArray();
            return new string(chars);
        }
    }
}