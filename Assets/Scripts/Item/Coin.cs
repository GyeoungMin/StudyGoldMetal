public class Coin : ItemController, IItem
{
    public void Get()
    {
        FindObjectOfType<UIManager>().AddScore(100);
    }
}
