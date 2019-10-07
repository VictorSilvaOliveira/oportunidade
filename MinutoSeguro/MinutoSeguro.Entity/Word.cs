namespace MinutoSeguro.Entity
{
    public class Word
    {

        private readonly string rawWord;

        public Word(string word)
        {
            this.rawWord = word;
        }

        public WordType Type { get; set; }

        public static implicit operator Word(string word)
        {
            return new Word(word);
        }


        public static implicit operator string(Word me)
        {
            return me.rawWord;
        }

    }
}