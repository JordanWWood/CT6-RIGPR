
public class Config {
    public string name;
    public int beatsPerMinute;
    public int beatsPerRow;
    public Duration duration;
    public bool[][] map;
    
    public class Duration {
        public int minutes;
        public int seconds;
    }
}