namespace AdvancedScada.Common
{
    public interface IDriverAdapter
    {
        bool IsConnected { get; set; }


        bool Connection();
        bool Disconnection();
        TValue[] Read<TValue>(string address, ushort length);
        TValue Read<TValue>(string address);
        bool Write(string address, dynamic value);

    }
}
