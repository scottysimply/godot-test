namespace TestProject.Utilities
{
    public class HealthChangeArgs
    {
        public HealthChangeArgs(int oldHealth, int newHealth, string? context = null) {
            OldHealth = oldHealth;
            NewHealth = newHealth;
            Context = context;
        }
        public int OldHealth { get; set; }
        public int NewHealth { get; set; }
        public string? Context { get; set; }
    }
}