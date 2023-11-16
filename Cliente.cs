using System;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;

public class Program
{
    private const int ServerPort = 8080;

    static bool IsValidPacket(string packetData)
    {
        try
        {
            using (var conn = new MySqlConnection("server=localhost;Database=devstatus;port=3306;User=root;Password=123456;"))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT validate_packet(@packetData)", conn))
                {
                    cmd.Parameters.AddWithValue("@packetData", packetData);
                    var result = (bool)cmd.ExecuteScalar();
                    return result;
                }
            }
        }
        catch (MySqlException e)
        {
            Console.WriteLine($"Erro ao conectar ou executar a consulta: {e.Message}");
            return false;
        }
    }

    static void SendPacket(string packet)
    {
        using (var udpClient = new UdpClient())
        {
            var serverEndpoint = new IPEndPoint(IPAddress.Loopback, ServerPort);
            var messageBytes = System.Text.Encoding.UTF8.GetBytes(packet);
            udpClient.Send(messageBytes, messageBytes.Length, serverEndpoint);
            Console.WriteLine($"Pacote enviado para o servidor: {packet}");
        }
    }

    

    static string GenerateRandomPacket()
    {
        Random random = new Random();

        int type = random.Next(1, 3); // Gera um número aleatório entre 1 e 2
        int protocolo = random.Next(66, 69); // Gera um número aleatório entre 66 e 68
        DateTime data = DateTime.Now;
        int status = random.Next(2); // Gera um número aleatório entre 0 e 1
        string id = GenerateRandomId();

        return $">DATA{type},{protocolo},{data:yyMMddHHmmss},{status};ID={id}<";
    }

    static string GenerateRandomId()
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] idArray = new char[3];
        Random random = new Random();

        for (int i = 0; i < 3; i++)
        {
            idArray[i] = characters[random.Next(characters.Length)];//to pegando 1 caracter entre a posição 0 e a 63 aonde 0 é o A e o 63 seria o 9
        }

        return new string(idArray);
    }

    static void Main()
    {
        try
        {
            // Loop infinito para enviar pacotes aleatórios a cada 5 segundos
                while (true)
                {
                    string randomPacket = GenerateRandomPacket();
                    SendPacket(randomPacket);

                    // Aguarda 5 segundos antes de enviar o próximo conjunto de dados
                    System.Threading.Thread.Sleep(5000);
        
                }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
