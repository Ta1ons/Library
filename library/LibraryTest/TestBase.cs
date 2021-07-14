namespace LibraryTest
{
    public class TestBase : ITestBase
    {
        public void InitilizeDb()
        {

        }
    }

    public interface ITestBase
    {
        void InitilizeDb();
    }
}
