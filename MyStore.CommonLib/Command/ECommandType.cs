namespace MyStore.CommonLib
{
    public enum ECommandType
    {
        Login = 0,
        CreateCustomer = 1,
        GetCustomerInfo = 2,
        ListAllCars = 3,
        ListAllCarsByName = 4,
        ListAllCarsByNameAndYear = 5,
        ListAllCarsByYear = 6,
        AddItemToCart = 7,
        GetItemsInCart = 8,
    }
}
