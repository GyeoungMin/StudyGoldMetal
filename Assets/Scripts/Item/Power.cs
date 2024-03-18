public class Power : ItemController, IItem
{
    public void Get()
    {
        FindObjectOfType<PlayerController>().PowerUp();
    }
}
