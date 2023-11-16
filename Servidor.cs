// Importações de namespaces necessários
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data.SQLite;
using System.Text.Json;
using System.Data.SqlClient;
// Definição da classe principal do servidor
public class SocketServer
{
    // Constantes para configurações do servidor
    private const int PORT = 8080;
    private const int MAX_CLIENTS = 20;

    // Lista para armazenar sockets dos clientes
    private static List<Socket> clients = new List<Socket>();

    // Flag para controle do loop principal do servidor
    private static bool running = true;

    // Conexão com o banco de dados SQLite
    private static SQLiteConnection dbConnection;

    // Cliente UDP para receber dados
    private static UdpClient udpServer;

    // Método principal que inicia o servidor
    public static void Main()
    {
        // Inicializa o banco de dados SQLite
        InitializeDatabase();

        // Converte o endereço IP para um formato adequado
        IPAddress ip = IPAddress.Parse("127.0.0.1");

        // Inicializa o cliente UDP para receber dados na porta especificada
        UdpClient udpServer = new UdpClient(PORT);

        // Mensagem indicando que o servidor está rodando
        Console.WriteLine("Servidor conectado...");

        // Loop principal do servidor
        while (running)
        {
            // Aguarda a recepção de dados
            IPEndPoint clientEndpoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] buffer = udpServer.Receive(ref clientEndpoint);

            // Adiciona o socket do cliente à lista, se houver espaço
            if (clients.Count < MAX_CLIENTS)
            {
                clients.Add(udpServer.Client);
                Console.WriteLine("Cliente conectado: " + clientEndpoint);

                // Inicia uma nova thread para lidar com os dados do cliente
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                clientThread.Start(new Tuple<IPEndPoint, byte[]>(clientEndpoint, buffer));
            }
            else
            {
                Console.WriteLine("Máximo de clientes atingido");
            }
        }
    }

    
    private static void InitializeDatabase()
    {
        // Certifica-se de ajustar o caminho do arquivo de banco de dados conforme necessário
        string connectionString = "server=localhost;database=devstatus;port=3306;user=root;password=123456;";
        MySqlConnection dbConnection = new MySqlConnection(connectionString);
        

try
        {
            // Abre a conexão com o banco de dados
            dbConnection.Open();

            // Cria a tabela 'dev_status' se não existir
            using (MySqlCommand command = new MySqlCommand(
                "CREATE TABLE IF NOT EXISTS dev_status (" +
                "type INT, " +
                "protocolo INT, " +
                "utc DATETIME, " +
                "status INT, " +
                "id VARCHAR(255));", dbConnection))
            {
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao inicializar o banco de dados: " + ex.Message);
        }
        finally
        {
            // Certifique-se de fechar a conexão quando terminar
            dbConnection.Close();
        }
    }

    // Função que lida com os dados recebidos de um cliente
    private static void HandleClient(object obj)
    {
        // Converte o objeto de parâmetros
        Tuple<IPEndPoint, byte[]> clientData = (Tuple<IPEndPoint, byte[]>)obj;
        IPEndPoint clientEndpoint = clientData.Item1;
        byte[] buffer = clientData.Item2;//aqui to pegando o array de byte
      
        try
        {

            // Obtém o número de bytes recebidos
            int receivedBytes = buffer.Length;

            // Verifica se pelo menos um byte foi recebido
            if (receivedBytes > 0)
            {
                // Converte os bytes para uma string usando UTF-8
                string message = Encoding.UTF8.GetString(buffer, 0, receivedBytes);


                string[] commandParts = message.Split(',');

                int dataType = int.Parse(commandParts[0].Substring(5));//nessa parte eu estou pegando >data e so pegando o valor


                int protocol = int.Parse(commandParts[1]);

                string utc = commandParts[2];

    
                string[] finalCommand = commandParts[3].Split(';');
                int status = int.Parse(finalCommand[0].Substring(0, 1));

                string id = finalCommand[1].Substring(3, 3);//aqui eu to tirando os id< e to pegando os 3 digitos do id

                // Armazenar os dados em um arquivo JSON

                var armazenaDados = new ArmazenaDados
                {
                    type = dataType,
                    protocolo = protocol,
                    utc = DateTime.ParseExact(utc, "yyMMddHHmmss", null),
                    status = status,
                    id = id
                };

                string jsonString = JsonSerializer.Serialize(armazenaDados);
                Console.WriteLine(jsonString);
                File.AppendAllLines("./dev_status.json", new[] { jsonString });

                InsertDataIntoDatabase(armazenaDados);
            }
        }
        catch (Exception ex)
        {
            // Imprime mensagem de erro
            Console.WriteLine("Erro ao processar mensagem: " + ex.Message);
        }
    }



    // Função que insere dados na tabela 'dev_status' do banco de dados
    private static void InsertDataIntoDatabase(ArmazenaDados armazenaDados)
    {
        
        // Certifique-se de ajustar as informações de conexão conforme necessário
        string connectionString = "server=localhost;database=devstatus;port=3306;user=root;password=123456;";
        MySqlConnection dbConnection = new MySqlConnection(connectionString);

        try
        {
            // Abre a conexão com o banco de dados
            dbConnection.Open();

            // Cria o comando SQL para inserção de dados
            using (MySqlCommand command = new MySqlCommand(
                "INSERT INTO dev_status (type, protocolo, utc, status, id) " +
                "VALUES (@type, @protocolo, @utc, @status, @id);", dbConnection))
            {
                // Adiciona parâmetros ao comando
                command.Parameters.AddWithValue("@Type",armazenaDados.type);
                command.Parameters.AddWithValue("@protocolo",armazenaDados.protocolo);
                command.Parameters.AddWithValue("@utc",armazenaDados.utc);
                command.Parameters.AddWithValue("@status", armazenaDados.status);
                command.Parameters.AddWithValue("@id", armazenaDados.id);

                // Executa o comando de inserção
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao inserir dados no banco de dados: " + ex.Message);
        }
        finally
        {
            // Certifique-se de fechar a conexão quando terminar
            dbConnection.Close();
        }
    }
}
    public class ArmazenaDados
{
    public int type { get; set; }
    public int protocolo { get; set; }
    public DateTime utc { get; set; }
    public int status { get; set; }
    public string id { get; set; }
}

