using Paraglider.Domain.Abstractions;

namespace Paraglider.Domain.Entities
{
    public class Review : IHaveId
    {
        public Guid Id { get; set; }
        public string Author { get; set; } = null!;
        public string AvatarUrl { get; set; } = null!;
        public string? Text { get; set; }

        private double evaluation;
        public double Evaluation 
        { 
            get => evaluation; 
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The evaluation cannot be lower than zero");
                }
                if (value > 5)
                {
                    throw new ArgumentException("A grade cannot be higher than five");
                }
                evaluation = value;
            }
        }
    }
}
