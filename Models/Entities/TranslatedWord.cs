using Models.Entities.Interfaces;

namespace Models.Entities
{
    public class TranslatedWord : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WordId { get; set; }
        public Word Word { get; set; }
    }
}
