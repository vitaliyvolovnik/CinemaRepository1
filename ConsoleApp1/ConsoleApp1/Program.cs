using System.Text;

string password = "↨◄♥¶↕▼";
string key = "fg134h41k2gghj413hjkc13c4g12hjgc43h1jkg4ch3j124c312y4ch12kc4k31fug21hcj4g32ty4cu31hjg12yuc432";
var len = key.Length;
int KPos = 0;

byte[] bytePassword = Encoding.UTF8.GetBytes(password);
byte[] byteKey = Encoding.UTF8.GetBytes(key);
List<byte> list = new();
foreach (var bt in bytePassword)
{
    list.Add(Convert.ToByte(bt ^ byteKey[KPos]));
}
Console.WriteLine(Encoding.UTF8.GetString(list.ToArray()));
Console.Read();
