namespace Domain
{
    public interface IShouldBeTracked
    {
        public Location? LastKnownLocation { get; set; }
    }
}
