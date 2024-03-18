public class Boom : ItemController, IItem
{
    public void Get()
    {
        FindObjectOfType<PlayerController>().AddBoom();
    }
}
