namespace Sharperio.Models.Dto.Fight
{
    public class HighScoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Fights { get; set; }
        public int Defets { get; set; }
        public int Victories { get; set; }
    }
}