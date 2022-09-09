
namespace Go2Climb.API.Resources
{
    public class ActivityResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ServiceResource Service;
    }
}