using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class SequenceRecognition<TElement>
    {
        private readonly TElement[] _elements;

        public SequenceRecognition(IEnumerable<TElement> elements)
        {
            _elements = elements.ToArray();
        }

        public RecognitionResult<TBlock> Recognize<TBlock>(params TBlock[] blocks)
            where TBlock : IEnumerable<TElement>
        {
            var applicants = blocks.ToList();
            var applicantIndex = 0;

            for (var i = 0; i < _elements.Length; i++)
            {
                var element = _elements[i];

                applicants = FilterApplicants(applicants, element, applicantIndex);
                if (applicants.Count == 0)
                {
                    i -= applicantIndex; 
                    applicantIndex = 0;
                    applicants = blocks.ToList();
                    continue;
                }

                var first = applicants.FirstOrDefault(x => x.Count() == applicantIndex + 1);
                if (!(first is null)) 
                    return new RecognitionResult<TBlock>(i - applicantIndex, first);

                applicantIndex++;
            }

            return RecognitionResult<TBlock>.NotFound;
        }

        private List<TBlock> FilterApplicants<TBlock>(List<TBlock> applicants, TElement element, int applicantIndex)
            where TBlock : IEnumerable<TElement>
        {
            return applicants
                .Where(applicant => applicant.ElementAt(applicantIndex).Equals(element))
                .ToList();
        }
    }
}