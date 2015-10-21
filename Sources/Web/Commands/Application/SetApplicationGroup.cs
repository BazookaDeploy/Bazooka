namespace Web.Commands
{
    public class SetApplicationGroup : ICommand
    {
        public int ApplicationId { get; set; }

        public int? ApplicationGroupId { get; set; }
    }
}
