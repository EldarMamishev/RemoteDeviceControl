namespace Infrastructure
{
    public class RecognitionResult<TBlock>
    {
        private static readonly int NotFoundIndex = -1;

        public static readonly RecognitionResult<TBlock> NotFound = new RecognitionResult<TBlock>(NotFoundIndex, default);

        public readonly int Index;

        public readonly TBlock Result;

        public RecognitionResult(int index, TBlock result)
        {
            Index = index;
            Result = result;
        }

        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is RecognitionResult<TBlock>))
                return false;

            var other = obj as RecognitionResult<TBlock>;

            return other.Index == Index && other.Result.Equals(Result);
        }

        public override int GetHashCode()
        {
            return Index.GetHashCode() * 17 + Result.GetHashCode();
        }

        public override string ToString()
        {
            return Index == NotFoundIndex ? "Not found" : $"{Index} - {Result}";
        }
    }
}