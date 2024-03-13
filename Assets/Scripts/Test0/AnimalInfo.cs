public class AnimalInfo
{

    //public Dictionary<string, object> AdditionalProperties { get; } = new Dictionary<string, object>();
    //public virtual T AdditionalProperty { get; }
    public string name { get; set; }
    public int type { get; set; }
    public int moveSpeed { get; set; }

    //»ý¼ºÀÚ
    public AnimalInfo(string name, int type, int moveSpeed)
    {
        this.name = name;
        this.type = type;
        this.moveSpeed = moveSpeed;
    }
}
