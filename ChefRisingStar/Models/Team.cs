
namespace ChefRisingStar.Models
{
    internal class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaptainId { get; set; }
        public bool Active { get; set; }
    }
}
